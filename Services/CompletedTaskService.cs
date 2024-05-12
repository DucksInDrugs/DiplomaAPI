using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class CompletedTaskService : ICompletedTaskService
    {
        private readonly ICompletedTaskRepository _repository;

        public CompletedTaskService(ICompletedTaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(CompletedTask task)
        {
            return await _repository.Create(task);
        }

        public async Task<bool> Delete(int userId)
        {
            return await _repository.Delete(userId);
        }

        public async Task<IEnumerable<CompletedTask>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<CompletedTask> GetByIds(int userId, int categoryId)
        {
            return await _repository.GetByIds(userId, categoryId);
        }
    }
}
