using Dapper;
using DiplomaAPI.Authorization.Interfaces;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;

namespace DiplomaAPI.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, DapperContext dataContext, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                using (IDbConnection db = dataContext.CreateConnection())
                {
                    User account = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"Users\" WHERE Id = @Id", new { Id = accountId.Value });

                    // attach account to context on successful jwt validation
                    context.Items["User"] = account;
                }
            }

            await _next(context);
        }
    }
}
