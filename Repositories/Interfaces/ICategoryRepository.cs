using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<int> Create(Category category);
        Task<bool> Update(Category category);
        Task<bool> Delete(int id);
    }
}
