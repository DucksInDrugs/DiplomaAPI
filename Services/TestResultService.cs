using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _repository;

        public TestResultService(ITestResultRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(TestResult model)
        {
            return await _repository.Create(model);
        }

        public async Task<IEnumerable<TestResult>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<TestResult>> GetByCategory(int categoryId)
        {
            return await _repository.GetByCategory(categoryId);
        }

        public async Task<IEnumerable<TestResult>> GetByGroup(int groupId)
        {
            return await _repository.GetByGroup(groupId);
        }

        public async Task<IEnumerable<TestResult>> GetByUser(int userId)
        {
            return await _repository.GetByUser(userId);
        }
    }
}
