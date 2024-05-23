namespace DiplomaAPI.Entities
{
    public class TestResult
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public DateTime? PassDate { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? GroupId { get; set; }
        public string? Username { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
    }
}
