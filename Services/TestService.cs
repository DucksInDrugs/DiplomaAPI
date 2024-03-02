using DiplomaAPI.Entities;
using DiplomaAPI.Repositories;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class TestService : ITestService
    {
        private readonly TestRepository _repository;

        public TestService(TestRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(Test model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Test>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Test>> GetByCategory(int categoryId)
        {
            return await _repository.GetByCategory(categoryId);
        }

        public async Task<Test> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(int id, Test model)
        {
            return await _repository.Update(model);
        }
    }
}
