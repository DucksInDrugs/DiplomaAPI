namespace DiplomaAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Role Role { get; set; }
        public string PhotoUrl { get; set; }
    }
}
