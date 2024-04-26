using DiplomaAPI.Entities;
using System.Security.Principal;

namespace DiplomaAPI.Authorization.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User account);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
