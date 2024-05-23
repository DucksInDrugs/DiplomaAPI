using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace DiplomaAPI.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DapperContext _context;

        public TestRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Test test)
        {
            string json = JsonConvert.SerializeObject(test.TestBody);
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"Tests\" (TestBody, CategoryId, Question, TestName, ImageUrl, VideoUrl) VALUES (@TestBody::jsonb, @CategoryId, @Question, @TestName, @ImageUrl, @VideoUrl);";
                return await db.ExecuteScalarAsync<int>(query, new { TestBody = json, test.CategoryId, test.Question, test.TestName, test.ImageUrl, test.VideoUrl });
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"Tests\" WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT id, testbody, categoryid, question, testname, videoUrl, imageUrl FROM \"Tests\"";
                var dynamicTests = await db.QueryAsync<(int Id, string TestBody, int CategoryId, string Question, string TestName, string VideoUrl, string ImageUrl)>(query);
                List<Test> tests = new List<Test>();
                foreach (var test in dynamicTests)
                {
                    tests.Add(new Test(test.Id, test.CategoryId, test.Question, JsonConvert.DeserializeObject<List<Answer>>(test.TestBody), test.TestName, test.ImageUrl, test.VideoUrl));
                }
                return tests.ToList();
            }
        }

        public async Task<IEnumerable<Test>> GetByCategory(int categoryId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT id, testbody, categoryid, question, testname, videoUrl, imageUrl FROM \"Tests\" WHERE CategoryId = @CategoryId";
                var dynamicTests = await db.QueryAsync<(int Id, string TestBody, int CategoryId, string Question, string TestName, string VideoUrl, string ImageUrl)>(query, new { CategoryId = categoryId});
                List<Test> tests = new List<Test>();
                foreach (var (Id, TestBody, CategoryId, Question, TestName, VideoUrl, ImageUrl) in dynamicTests)
                {
                    tests.Add(new Test(Id, CategoryId, Question, JsonConvert.DeserializeObject<List<Answer>>(TestBody), TestName, ImageUrl, VideoUrl));
                }
                return tests.ToList();
            }
        }

        public async Task<Test> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                var tupleTest = await db.QueryFirstOrDefaultAsync<(int Id, string Question, string TestBody, string TestName, int CategoryId, string VideoUrl, string ImageUrl)>("SELECT * FROM \"Tests\" WHERE Id = @Id", new { Id = id });
                return new Test(tupleTest.Id, tupleTest.CategoryId, tupleTest.Question, JsonConvert.DeserializeObject<List<Answer>>(tupleTest.TestBody), tupleTest.TestName, tupleTest.ImageUrl, tupleTest.VideoUrl);
            }
        }

        public async Task<bool> Update(Test test)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                string json = JsonConvert.SerializeObject(test.TestBody);
                const string query = "UPDATE \"Tests\" SET TestBody = @TestBody::jsonb, CategoryId = @CategoryId, Question = @Question, TestName = @TestName, VideoUrl = @VideoUrl, ImageUrl = @ImageUrl WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { TestBody = json, test.CategoryId, test.Question, test.TestName, test.VideoUrl, test.ImageUrl, test.Id });
                return rowsAffected > 0;
            }
        }
    }
}
