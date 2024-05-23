using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ITestResultRepository
    {
        Task<IEnumerable<TestResult>> GetAll();
        Task<IEnumerable<TestResult>> GetByCategory(int categoryId);
        Task<IEnumerable<TestResult>> GetByGroup(int groupId);
        Task<IEnumerable<TestResult>> GetByUser(int userId);
        Task<int> Create(TestResult model);
    }
}
