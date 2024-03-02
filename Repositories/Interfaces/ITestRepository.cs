using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAll();
        Task<Test> GetById(int id);
        Task<IEnumerable<Test>> GetByCategory();
        Task<bool> Create(Test user);
        Task<bool> Update(Test user);
        Task<bool> Delete(int id);
    }
}
