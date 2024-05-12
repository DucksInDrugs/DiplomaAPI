using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class CompletedTaskRepository : ICompletedTaskRepository
    {
        private readonly DapperContext _context;

        public CompletedTaskRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CompletedTask task)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"CompletedTasks\" (UserId, CategoryId) VALUES (@UserId, @CategoryId);";
                return await db.ExecuteScalarAsync<int>(query, task);
            }
        }

        public async Task<bool> Delete(int userId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"CompletedTasks\" WHERE UserId = @UserId";
                int rowsAffected = await db.ExecuteAsync(query, new { UserId = userId });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<CompletedTask>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"CompletedTasks\"";
                IEnumerable<CompletedTask> tasks = await db.QueryAsync<CompletedTask>(query);
                return tasks.ToList();
            }
        }

        public async Task<CompletedTask> GetByIds(int userId, int categoryId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<CompletedTask>("SELECT * FROM \"CompletedTasks\" WHERE UserId = @UserId AND CategoryId = @CategoryId", new { UserId = userId, CategoryId = categoryId });
            }
        }
    }
}
