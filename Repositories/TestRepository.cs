using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

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
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO Tests (Smth) VALUES (@Smth); SELECT SCOPE_IDENTITY();";
                return await db.ExecuteScalarAsync<int>(query, test);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM Tests WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM Tests";
                IEnumerable<Test> tests = await db.QueryAsync<Test>(query);
                return tests.ToList();
            }
        }

        public async Task<IEnumerable<Test>> GetByCategory(int categoryId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM Tests WHERE CategoryId = @CategoryId";
                IEnumerable<Test> tests = await db.QueryAsync<Test>(query);
                return tests.ToList();
            }
        }

        public async Task<Test> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Test>("SELECT * FROM Tests WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<bool> Update(Test test)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE Tests SET Smth = @Smth WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, test);
                return rowsAffected > 0;
            }
        }
    }
}
