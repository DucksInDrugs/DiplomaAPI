using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface ITestService
    {
        Task<IEnumerable<Test>> GetAll();
        Task<IEnumerable<Test>> GetByCategory();
        Task<Test> GetById(int id);
        Task<bool> Create(Test model);
        Task<bool> Update(int id, Test model);
        Task<bool> Delete(int id);
    }
}
