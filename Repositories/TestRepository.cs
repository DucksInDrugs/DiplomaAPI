using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Repositories
{
    public class TestRepository : ITestRepository
    {
        public Task<bool> Create(Test user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Test>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Test>> GetByCategory()
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Test user)
        {
            throw new NotImplementedException();
        }
    }
}
