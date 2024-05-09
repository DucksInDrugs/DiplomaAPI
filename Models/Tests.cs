namespace DiplomaAPI.Models
{
    public class Tests
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<AnswerModel> TestBody { get; set; }
        public int CategoryId { get; set; }
        public string TestName { get; set; }

        public class AnswerModel
        {
            public string AnswerText { get; set; }
            public bool IsCorrect { get; set; }
        }
    }
}
