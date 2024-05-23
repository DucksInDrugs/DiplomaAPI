using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models.Users
{
    public class UploadProfilesRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Group { get; set; }
    }
}
