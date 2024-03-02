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

        public Task<bool> Create(Video user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Video>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Video>> GetByCategory()
        {
            throw new NotImplementedException();
        }

        public Task<Video> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Video user)
        {
            throw new NotImplementedException();
        }
    }
}
