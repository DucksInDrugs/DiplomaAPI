using AutoMapper;
using Dapper;
using DiplomaAPI.Authorization.Interfaces;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Models;
using DiplomaAPI.Models.Users;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DiplomaAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        //private readonly IEmailService _emailService;

        public UserRepository(
            DapperContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        //IEmailService emailService)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            //_emailService = emailService;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                User account = db.QueryFirstOrDefault<User>("SELECT * FROM \"Users\" WHERE Email = @Email", model );


                // validate
                if (account == null || !BCrypt.Net.BCrypt.Verify(model.Password, account.PasswordHash))
                    throw new Exception("Email or password is incorrect");

                // authentication successful so generate jwt and refresh tokens
                var jwtToken = _jwtUtils.GenerateJwtToken(account);
                var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
                refreshToken.UserId = account.Id;

                const string createQuery = "INSERT INTO \"RefreshTokens\" (UserId, Token, Expires, Created, CreatedByIp, Revoked, RevokedByIp, ReplacedByToken, ReasonRevoked) VALUES (@UserId, @Token, @Expires, @Created, @CreatedByIp, @Revoked, @RevokedByIp, @ReplacedByToken, @ReasonRevoked);";
                db.Execute(createQuery, refreshToken);

                account.RefreshTokens.Add(refreshToken);

                // remove old refresh tokens from account
                RemoveOldRefreshTokens(account);

                // save changes to db
                const string updateQuery = "UPDATE \"Users\" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, VerificationToken = @VerificationToken, ResetToken = @ResetToken, ResetTokenExpires = @ResetTokenExpires WHERE Id = @Id";
                db.Execute(updateQuery, account);



                var response = _mapper.Map<AuthenticateResponse>(account);
                response.JwtToken = jwtToken;
                response.RefreshToken = refreshToken.Token;
                return response;
            }            
        }

        public AccountResponse Create(CreateRequest model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Users\" WHERE Email = @Email";
                int rowsAffected = db.Execute(query, new { Email = model.Email });
                if (rowsAffected > 0)
                {
                    throw new Exception($"Email '{model.Email}' is already registered");
                }

                // map model to new account object
                var account = _mapper.Map<User>(model);
                /*account.Created = DateTime.UtcNow;
                account.Verified = DateTime.UtcNow;*/

                // hash password
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                const string createQuery = "INSERT INTO \"Users\" (Username, Email, PasswordHash, Role, VerificationToken, ResetToken, ResetTokenExpires) VALUES (@Username, @Email, @PasswordHash, @Role, @VerificationToken, @ResetToken, @ResetTokenExpires);";
                db.Execute(createQuery, account);

                return _mapper.Map<AccountResponse>(account);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"Users\" WHERE Id = @Id";
                int rowsAffected = db.Execute(query, new { Id = id });
            }
        }

        public IEnumerable<AccountResponse> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Users\"";

                IEnumerable<User> users = db.Query<User>(query);
                return _mapper.Map<IList<AccountResponse>>(users);
            }
        }

        public AccountResponse GetById(int id)
        {
            var account = GetAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var account = GetAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                RevokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                using (IDbConnection db = _context.CreateConnection())
                {
                    const string updateQuery = "UPDATE \"Users\" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, VerificationToken = @VerificationToken, ResetToken = @ResetToken, ResetTokenExpires = @ResetTokenExpires WHERE Id = @Id";
                    db.Execute(updateQuery, account);
                }
            }

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from account
            RemoveOldRefreshTokens(account);

            // save changes to db
            using (IDbConnection db = _context.CreateConnection())
            {
                const string updateQuery = "UPDATE \"Users\" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, VerificationToken = @VerificationToken, ResetToken = @ResetToken, ResetTokenExpires = @ResetTokenExpires WHERE Id = @Id";
                db.Execute(updateQuery, account);
            }

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            // return data in authenticate response object
            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public void Register(RegisterRequest model, string origin)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Users\"";

                IEnumerable<User> users = db.Query<User>(query);
                // validate
                if (users.Any(x => x.Email == model.Email))
                {
                    throw new Exception($"Email '{model.Email}' is already registered");
                }

                // map model to new account object
                var account = _mapper.Map<User>(model);

                // first registered account is an admin
                var isFirstAccount = users.Count() == 0;
                account.Role = isFirstAccount ? Role.Admin : Role.User;

                // hash password
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                const string createQuery = "INSERT INTO \"Users\" (Username, Email, PasswordHash, Role, VerificationToken, ResetToken, ResetTokenExpires) VALUES (@Username, @Email, @PasswordHash, @Role, @VerificationToken, @ResetToken, @ResetTokenExpires);";
                db.Execute(createQuery, account);
            }
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var account = GetAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new Exception("Invalid token");

            // revoke token and save
            RevokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            using (IDbConnection db = _context.CreateConnection())
            {
                const string updateQuery = "UPDATE \"Users\" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, VerificationToken = @VerificationToken, ResetToken = @ResetToken, ResetTokenExpires = @ResetTokenExpires WHERE Id = @Id";
                db.Execute(updateQuery, account);
            }
        }

        public AccountResponse Update(int id, UpdateRequest model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                var account = GetAccount(id);
                const string query = "SELECT * FROM \"Users\" WHERE Email = @Email";

                IEnumerable<User> users = db.Query<User>(query, new { Email = model.Email } );

                // validate
                if (account.Email != model.Email && users.Any())
                    throw new Exception($"Email '{model.Email}' is already registered");

                // hash password if it was entered
                if (!string.IsNullOrEmpty(model.Password))
                    account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // copy model to account and save
                _mapper.Map(model, account);

                const string updateQuery = "UPDATE \"Users\" SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Role = @Role, VerificationToken = @VerificationToken, ResetToken = @ResetToken, ResetTokenExpires = @ResetTokenExpires WHERE Id = @Id";
                db.Execute(updateQuery, account);

                return _mapper.Map<AccountResponse>(account);
            }
        }

/*        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            GetAccountByResetToken(model.Token);
        }*/

        //

        private User GetAccount(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                User account = db.QueryFirstOrDefault<User>("SELECT * FROM \"User\" WHERE Id = @Id", new { Id = id});
                if (account == null) throw new KeyNotFoundException("Account not found");
                return account;
            }            
        }

        private User GetAccountByRefreshToken(string token)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                RefreshToken? refreshToken = db.QueryFirstOrDefault<RefreshToken>("SELECT * FROM \"RefreshTokens\" WHERE Token = @Token", new { Token = token });
                if (refreshToken == null)
                {
                    throw new Exception("Invalid token");
                }
                User account = GetAccount(refreshToken.UserId);

                return account;
            }
/*                var account = _context.Accounts.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (account == null) throw new Exception("Invalid token");*/
            
        }

/*        private User GetAccountByResetToken(string token)
        {
            var account = _context.Accounts.SingleOrDefault(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
            if (account == null) throw new Exception("Invalid token");
            return account;
        }*/

/*        private string GenerateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }*/

/*        private string GenerateResetToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.Accounts.Any(x => x.ResetToken == token);
            if (!tokenIsUnique)
                return GenerateResetToken();

            return token;
        }*/

        private string GenerateVerificationToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Users\" WHERE VerificationToken = @VerificationToken";

                IEnumerable<User> users = db.Query<User>(query, new { VerificationToken = token });
                var tokenIsUnique = !users.Any();
                if (!tokenIsUnique)
                    return GenerateVerificationToken();

                return token;
            }
                
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void RemoveOldRefreshTokens(User account)
        {
            List<RefreshToken> tokensToDelete = account.RefreshTokens.FindAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);

            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"RefreshTokens\" WHERE Id = @Id";
                foreach (RefreshToken token in tokensToDelete)
                {
                    db.Query<RefreshToken>(query, new { Id = token.Id });
                }
            }

                account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, User account, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
    }
}
