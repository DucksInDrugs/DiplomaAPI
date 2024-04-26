using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models.Users
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
