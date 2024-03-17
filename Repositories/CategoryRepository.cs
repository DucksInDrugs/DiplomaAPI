using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;

        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Category category)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"Categories\" (Title, PhotoUrl) VALUES (@Title, @PhotoUrl); SELECT SCOPE_IDENTITY();";
                return await db.ExecuteScalarAsync<int>(query, category);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"Categories\" WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Categories\"";
                IEnumerable<Category> categories = await db.QueryAsync<Category>(query);
                return categories.ToList();
            }
        }

        public async Task<Category> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Category>("SELECT * FROM \"Categories\" WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<bool> Update(Category category)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE \"Categories\" SET Title = @Title, PhotoUrl = @PhotoUrl WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, category);
                return rowsAffected > 0;
            }
        }
    }
}
