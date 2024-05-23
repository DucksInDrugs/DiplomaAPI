using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface ITestResultService
    {
        Task<IEnumerable<TestResult>> GetAll();
        Task<IEnumerable<TestResult>> GetByCategory(int categoryId);
        Task<IEnumerable<TestResult>> GetByGroup(int groupId);
        Task<IEnumerable<TestResult>> GetByUser(int userId);
        Task<int> Create(TestResult model);
    }
}
