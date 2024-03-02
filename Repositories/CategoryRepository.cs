using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<bool> Create(Category user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Category user)
        {
            throw new NotImplementedException();
        }
    }
}
