using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface ICompletedTaskService
    {
        Task<IEnumerable<CompletedTask>> GetAll();
        Task<CompletedTask> GetByIds(int userId, int categoryId);
        Task<int> Create(CompletedTask task);
        Task<bool> Delete(int userId);
    }
}
