using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class RandomTaskService : IRandomTaskService
    {
        private readonly IRandomTaskRepository _repository;

        public RandomTaskService(IRandomTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(RandomTask model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<RandomTask>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<RandomTask> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(int id, RandomTask model)
        {
            return await _repository.Update(id, model);
        }
    }
}
