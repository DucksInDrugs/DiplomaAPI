using DiplomaAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models.Users
{
    public class CreateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int? GroupId { get; set; }

        public CreateRequest(string username, string role, string email, string password, string confirmPassword, int? groupId)
        {
            Username = username;
            Role = role;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            GroupId = groupId;
        }
    }
}
