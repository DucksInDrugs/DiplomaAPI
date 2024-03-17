using DiplomaAPI.Entities;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;
using System.Reflection;

namespace DiplomaAPI.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _repository;

        public VideoService(IVideoRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(Video model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Video>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Video>> GetByCategory(int categoryId)
        {
            return await _repository.GetByCategory(categoryId);
        }

        public async Task<Video> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(int id, Video model) //TODO: Какой-то прикол с id в Update, разобраться
        {
            return await _repository.Update(model);
        }
    }
}
