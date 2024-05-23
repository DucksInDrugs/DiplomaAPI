using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(Group model)
        {
            return await _repository.Create(model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Group> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(int id, Group model)
        {
            return await _repository.Update(id, model);
        }
    }
}
