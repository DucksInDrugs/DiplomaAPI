using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;
using System.Xml;

namespace DiplomaAPI.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DapperContext _context;

        public ArticleRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Article article)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "INSERT INTO \"Articles\" (Title, Description, Role, GroupId, ImageUrl, HtmlText) VALUES (@Title, @Description, @Role, @GroupId, @ImageUrl, @HtmlText);";
                return await db.ExecuteScalarAsync<int>(query, article);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"Articles\" WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Articles\"";
                IEnumerable<Article> articles = await db.QueryAsync<Article>(query);
                return articles.ToList();
            }
        }

        public async Task<IEnumerable<Article>> GetByGroupId(int groupId)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Articles\" WHERE GroupId = @GroupId";
                IEnumerable<Article> articles = await db.QueryAsync<Article>(query, new { GroupId = groupId });
                return articles.ToList();
            }
        }

        public async Task<Article> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Article>("SELECT * FROM \"Articles\" WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<Article>> GetByRole(string role)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                int roleId = (int)Enum.Parse(typeof(Role), role);
                const string query = "SELECT * FROM \"Articles\" WHERE Role = @Role";
                IEnumerable<Article> articles = await db.QueryAsync<Article>(query, new { Role = roleId });
                return articles.ToList();
            }
        }

        public async Task<IEnumerable<Article>> GetFirstArticles()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Articles\" ORDER BY id DESC LIMIT 4";
                IEnumerable<Article> articles = await db.QueryAsync<Article>(query);
                return articles.ToList();
            }
        }

        public async Task<bool> Update(Article article)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE \"Articles\" SET Title = @Title, Description = @Description, Role = @Role, GroupId = @GroupId, ImageUrl = @ImageUrl, HtmlText = @HtmlText WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, article);
                return rowsAffected > 0;
            }
        }
    }
}
