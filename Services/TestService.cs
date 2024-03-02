using DiplomaAPI.Entities;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class TestService : ITestService
    {
        public Task<bool> Create(Test model)
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

        public Task<bool> Update(int id, Test model)
        {
            throw new NotImplementedException();
        }
    }
}
