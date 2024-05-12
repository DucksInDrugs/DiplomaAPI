using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ICompletedTaskRepository
    {
        Task<IEnumerable<CompletedTask>> GetAll();
        Task<CompletedTask> GetByIds(int userId, int categoryId);
        Task<int> Create(CompletedTask task);
        Task<bool> Delete(int userId);
    }
}
