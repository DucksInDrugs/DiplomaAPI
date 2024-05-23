using DiplomaAPI.Entities;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.Services.Interfaces;

namespace DiplomaAPI.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Create(Article article)
        {
            return await _repository.Create(article);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Article>> GetByGroupId(int groupId)
        {
            return await _repository.GetByGroupId(groupId);
        }

        public async Task<Article> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<Article>> GetByRole(string role)
        {
            return await _repository.GetByRole(role);
        }

        public async Task<IEnumerable<Article>> GetFirstArticles()
        {
            return await _repository.GetFirstArticles();
        }

        public async Task<bool> Update(Article article)
        {
            return await _repository.Update(article);
        }
    }
}
