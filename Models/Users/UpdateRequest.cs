using DiplomaAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models.Users
{
    public class UpdateRequest
    {
        public string? Password { get; set; }
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }
    }
}
