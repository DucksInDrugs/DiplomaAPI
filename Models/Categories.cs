using DiplomaAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }
        public string PhotoUrl { get; set; }
    }
}
