using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class RandomTaskRepository : IRandomTaskRepository
    {
        private readonly DapperContext _context;

        public RandomTaskRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(RandomTask model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"RandomTasks\" (VideoUrl, ImageUrl, CorrectAnswer) VALUES (@VideoUrl, @ImageUrl, @CorrectAnswer);";
                return await db.ExecuteScalarAsync<int>(query, model);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"RandomTasks\" WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<RandomTask>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"RandomTasks\"";
                IEnumerable<RandomTask> tasks = await db.QueryAsync<RandomTask>(query);
                return tasks.ToList();
            }
        }

        public async Task<RandomTask> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<RandomTask>("SELECT * FROM \"RandomTasks\" WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<bool> Update(int id, RandomTask model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE \"RandomTasks\" SET VideoUrl = @VideoUrl, ImageUrl = @ImageUrl, CorrectAnswer = @CorrectAnswer WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, model);
                return rowsAffected > 0;
            }
        }
    }
}
