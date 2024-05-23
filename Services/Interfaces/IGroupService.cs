using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group> GetById(int id);
        Task<int> Create(Group model);
        Task<bool> Update(int id, Group model);
        Task<bool> Delete(int id);
    }
}
