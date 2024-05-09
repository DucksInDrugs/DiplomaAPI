namespace DiplomaAPI.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<Answer> TestBody { get; set; }
        public int CategoryId { get; set; }
        public string TestName { get; set; }

        public Test(int id, int categoryId, string question, List<Answer> testBody, string testName)
        {
            Id = id;
            Question = question;
            TestBody = testBody;
            CategoryId = categoryId;
            TestName = testName;
        }
    }

    public class Answer
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
