using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAll();
        Task<IEnumerable<Video>> GetByCategory();
        Task<Video> GetById(int id);
        Task<bool> Create(Video user);
        Task<bool> Update(Video user);
        Task<bool> Delete(int id);
    }
}
