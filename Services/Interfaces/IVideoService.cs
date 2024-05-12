using DiplomaAPI.Entities;

namespace DiplomaAPI.Services.Interfaces
{
    public interface IVideoService
    {
        Task<IEnumerable<Video>> GetAll();
        Task<Video> GetByCategory(int categoryId);
        Task<Video> GetById(int id);
        Task<int> Create(Video model);
        Task<bool> Update(int id, Video model);
        Task<bool> Delete(int id);
    }
}
