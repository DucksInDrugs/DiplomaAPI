using DiplomaAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public string ImageUrl { get; set; }
        [EnumDataType(typeof(Role))]
        public string Role { get; set; }
        public int GroupId { get; set; }
    }
}
