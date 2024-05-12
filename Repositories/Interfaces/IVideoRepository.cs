using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAll();
        Task<Video> GetByCategory(int categoryId);
        Task<Video> GetById(int id);
        Task<int> Create(Video video);
        Task<bool> Update(Video video);
        Task<bool> Delete(int id);
    }
}
