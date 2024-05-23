using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IRandomTaskRepository
    {
        Task<IEnumerable<RandomTask>> GetAll();
        Task<RandomTask> GetById(int id);
        Task<int> Create(RandomTask model);
        Task<bool> Update(int id, RandomTask model);
        Task<bool> Delete(int id);
    }
}
