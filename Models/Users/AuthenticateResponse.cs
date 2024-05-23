using System.Text.Json.Serialization;

namespace DiplomaAPI.Models.Users
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public double Progress { get; set; }
        public int? GroupId { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
