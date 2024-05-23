using DiplomaAPI.Entities;

namespace DiplomaAPI.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAll();
        Task<IEnumerable<Article>> GetFirstArticles();
        Task<Article> GetById(int id);
        Task<int> Create(Article article);
        Task<bool> Update(Article article);
        Task<bool> Delete(int id);
        Task<IEnumerable<Article>> GetByRole(string role);
        Task<IEnumerable<Article>> GetByGroupId(int groupId);
    }
}
