using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetFirstCategories();
        Task<Category> GetById(int id);
        Task<int> Create(Category model);
        Task<bool> Update(int id, Category model);
        Task<bool> Delete(int id);
        Task<IEnumerable<Category>> GetByRole(string role);
    }
}
