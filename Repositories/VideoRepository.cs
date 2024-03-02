using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using Npgsql;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly DapperContext _context;

        public VideoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Video video)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO Videos (Link, Title, Description, Author, PhotoUrl) VALUES (@Link, @Title, @Description, @Author, @PhotoUrl); SELECT SCOPE_IDENTITY();";
                return await db.ExecuteScalarAsync<int>(query, video);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM Videos WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Video>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM Videos";

                IEnumerable<Video> videos = await db.QueryAsync<Video>(query);
                return videos.ToList();
            }
        }

        public async Task<IEnumerable<Video>> GetByCategory(int categoryId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM Videos WHERE CategoryId = @CategoryId";
                IEnumerable<Video> videos = await db.QueryAsync<Video>(query);
                return videos.ToList();
            }
        }

        public async Task<Video> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Video>("SELECT * FROM Videos WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<bool> Update(Video video)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE Videos SET Link = @Link, Title = @Title, Description = @Description, Author = @Author, PhotoUrl = @PhotoUrl WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, video);
                return rowsAffected > 0;
            }
        }
    }
}
