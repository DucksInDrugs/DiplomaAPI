namespace DiplomaAPI.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public string ImageUrl { get; set; }
        public Role Role { get; set; }
        public int GroupId { get; set; }
    }
}
