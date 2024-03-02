using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<Video>> GetAll();
        Task<IEnumerable<Video>> GetByCategory();
        Task<Video> GetById(int id);
        Task<bool> Create(Video model);
        Task<bool> Update(int id, Video model);
        Task<bool> Delete(int id);
    }
}
