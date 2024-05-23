using Dapper;
using DiplomaAPI.Entities;
using DiplomaAPI.Helpers;
using DiplomaAPI.Repositories.Interfaces;
using System.Data;

namespace DiplomaAPI.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DapperContext _context;

        public GroupRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Group model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string querySelect = "SELECT * FROM \"Groups\"";

                IEnumerable<Group> groups = db.Query<Group>(querySelect);

                if (groups.Any(x => x.Name == model.Name))
                {
                    throw new AppException("Группа уже была создана");
                }

                const string query = "INSERT INTO \"Groups\" (Name) VALUES (@Name);";
                return await db.ExecuteScalarAsync<int>(query, model);
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "DELETE FROM \"Groups\" WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "SELECT * FROM \"Groups\"";
                IEnumerable<Group> groups = await db.QueryAsync<Group>(query);
                return groups.ToList();
            }
        }

        public async Task<Group> GetById(int id)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                return await db.QueryFirstOrDefaultAsync<Group>("SELECT * FROM \"Groups\" WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<bool> Update(int id, Group model)
        {
            using (IDbConnection db = _context.CreateConnection())
            {
                const string query = "UPDATE \"Groups\" SET Name = @Name WHERE Id = @Id";
                int rowsAffected = await db.ExecuteAsync(query, model);
                return rowsAffected > 0;
            }
        }
    }
}
