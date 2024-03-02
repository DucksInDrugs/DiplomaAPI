using DiplomaAPI.Entities;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class VideoService : IVideoService
    {
        public Task<bool> Create(Video model)
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

        public Task<bool> Update(int id, Video model)
        {
            throw new NotImplementedException();
        }
    }
}
