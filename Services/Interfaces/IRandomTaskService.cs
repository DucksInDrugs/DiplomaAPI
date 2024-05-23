using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface IRandomTaskService
    {
        Task<IEnumerable<RandomTask>> GetAll();
        Task<RandomTask> GetById(int id);
        Task<int> Create(RandomTask model);
        Task<bool> Update(int id, RandomTask model);
        Task<bool> Delete(int id);
    }
}
