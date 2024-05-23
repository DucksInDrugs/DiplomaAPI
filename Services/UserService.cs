using AutoMapper;
using DiplomaAPI.Authorization.Interfaces;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Models.Users;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace DiplomaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            return _repository.Authenticate(model, ipAddress);
        }

        public AccountResponse Create(CreateRequest model)
        {
            return _repository.Create(model);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<AccountResponse> GetAll()
        {
            return _repository.GetAll();
        }

        public AccountResponse GetById(int id)
        {
            return _repository.GetById(id);
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            return _repository.RefreshToken(token, ipAddress);
        }

        public void Register(RegisterRequest model, string origin)
        {
            _repository.Register(model, origin);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            _repository.RevokeToken(token, ipAddress);
        }

        public AccountResponse Update(int id, UpdateRequest model)
        {
            return _repository.Update(id, model);
        }

        public AccountResponse UpdateProgress(int id, ProgressRequest model)
        {
            return _repository.UpdateProgress(id, model);
        }

        public void UploadProfiles(List<UploadProfilesRequest> model)
        {
            _repository.UploadProfiles(model);
        }

        /*        public void ValidateResetToken(ValidateResetTokenRequest model)
                {
                    _repository.ValidateResetToken(model);
                }*/

        /*private void sendVerificationEmail(User account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                // origin exists if request sent from browser single page app (e.g. Angular or React)
                // so send link to verify via single page app
                var verifyUrl = $"{origin}/account/verify-email?token={account.VerificationToken}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                            <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                // origin missing if request sent directly to api (e.g. from Postman)
                // so send instructions to verify directly with api
                message = $@"<p>Please use the below token to verify your email address with the <code>/accounts/verify-email</code> api route:</p>
                            <p><code>{account.VerificationToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                        <p>Thanks for registering!</p>
                        {message}"
            );
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                        <p>Your email <strong>{email}</strong> is already registered.</p>
                        {message}"
            );
        }

        private void sendPasswordResetEmail(User account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                            <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                        {message}"
            );
        }*/
    }
}
