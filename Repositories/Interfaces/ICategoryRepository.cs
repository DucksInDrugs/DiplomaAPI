using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<bool> Create(Category user);
        Task<bool> Update(Category user);
        Task<bool> Delete(int id);
    }
}
