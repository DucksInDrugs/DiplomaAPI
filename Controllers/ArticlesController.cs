using DiplomaAPI.Authorization;
using DiplomaAPI.Entities;
using DiplomaAPI.Models;
using DiplomaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articles>>> GetAll()
        {
            var articles = await _service.GetAll();
            return Ok(articles);
        }

        [HttpGet("FirstArticles")]
        public async Task<ActionResult<IEnumerable<Articles>>> GetFirstArticles()
        {
            var articles = await _service.GetFirstArticles();
            return Ok(articles);
        }

        [Authorize(Role.Admin, Role.SuperTeacher, Role.Teacher)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Articles>> GetById(int id)
        {
            var article = await _service.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [Authorize(Role.Admin, Role.SuperTeacher, Role.Teacher)]
        [HttpPost]
        public async Task<ActionResult<Articles>> Create(Article article)
        {
            var newArticleId = await _service.Create(article);
            article.Id = newArticleId;
            return CreatedAtAction(nameof(GetById), new { id = newArticleId }, article);
        }

        [Authorize(Role.Admin, Role.SuperTeacher, Role.Teacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Article article)
        {
            var existingArticle = await _service.GetById(id);
            if (existingArticle == null)
            {
                return NotFound();
            }

            article.Id = id;
            if (await _service.Update(article))
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, article);
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher, Role.Teacher)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingArticle = await _service.GetById(id);
            if (existingArticle == null)
            {
                return NotFound();
            }

            if (await _service.Delete(id))
            {
                return Ok(new { message = "Article deleted successfully" });
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpGet("ArticlesByRole/{role}")]
        public async Task<ActionResult<IEnumerable<Articles>>> GetByRole(string role)
        {
            var articles = await _service.GetByRole(role);
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles);
        }

        [HttpGet("ArticlesByGroup/{id}")]
        public async Task<ActionResult<IEnumerable<Articles>>> GetByGroupId(int id)
        {
            var articles = await _service.GetByGroupId(id);
            if (articles == null)
            {
                return NotFound();
            }
            return Ok(articles);
        }
    }
}
