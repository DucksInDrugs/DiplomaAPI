using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(int id);
        Task<IEnumerable<Test>> GetByCategory(int categoryId);
        Task<int> Create(Test test);
        Task<bool> Update(Test test);
        Task<bool> Delete(int id);
    }
}
