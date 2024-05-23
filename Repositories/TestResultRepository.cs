using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly DapperContext _context;

        public TestResultRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(TestResult model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"TestResults\" (UserId, Result, PassDate, CategoryId, GroupId) VALUES (@UserId, @Result, @PassDate, @CategoryId, @GroupId);";
                return await db.ExecuteScalarAsync<int>(query, model);
            }
        }

        public async Task<IEnumerable<TestResult>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = @"SELECT ""TestResults"".id, result, passdate, userid, categoryid, ""TestResults"".groupid, username, title, name FROM ""TestResults""
                                    LEFT JOIN ""Users"" ON ""Users"".id = ""TestResults"".userid
                                    LEFT JOIN ""Categories"" ON ""Categories"".id = ""TestResults"".categoryid
                                    LEFT JOIN ""Groups"" ON ""Groups"".id = ""TestResults"".groupid;";
                IEnumerable<TestResult> results = await db.QueryAsync<TestResult>(query);
                return results.ToList();
            }
        }

        public async Task<IEnumerable<TestResult>> GetByCategory(int categoryId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"TestResults\" WHERE CategoryId = @CategoryId";
                IEnumerable<TestResult> results = await db.QueryAsync<TestResult>(query, new { CategoryId = categoryId });
                return results.ToList();
            }
        }

        public async Task<IEnumerable<TestResult>> GetByGroup(int groupId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"TestResults\" WHERE GroupId = @GroupId";
                IEnumerable<TestResult> results = await db.QueryAsync<TestResult>(query, new { GroupId = groupId });
                return results.ToList();
            }
        }

        public async Task<IEnumerable<TestResult>> GetByUser(int userId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"TestResults\" WHERE UserId = @UserId";
                IEnumerable<TestResult> results = await db.QueryAsync<TestResult>(query, new { UserId = userId });
                return results.ToList();
            }
        }
    }
}
