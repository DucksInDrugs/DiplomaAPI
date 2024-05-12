using DiplomaAPI.Authorization;
using DiplomaAPI.Entities;
using DiplomaAPI.Models;
using DiplomaAPI.Services;
using DiplomaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase//Добавить гетпо категориям
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetAllCategories()
        {
            var categories = await _service.GetAll();
            return Ok(categories);
        }

        [HttpGet("FirstCategories")]
        public async Task<ActionResult<IEnumerable<Categories>>> GetFirstCategories()
        {
            var categories = await _service.GetFirstCategories();
            return Ok(categories);
        }

        [Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategoryById(int id)
        {
            var category = await _service.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Categories>> CreateCategory(Category category)
        {
            var newCategoryId = await _service.Create(category);
            category.Id = newCategoryId;
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            var existingCategory = await _service.GetById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            category.Id = id;
            if (await _service.Update(id, category))
            {
                return CreatedAtAction(nameof(GetCategoryById), new { id = id }, category);
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _service.GetById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            if (await _service.Delete(id))
            {
                return Ok(new { message = "Category deleted successfully" });
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpGet("CategoriesByRole/{role}")]
        public async Task<ActionResult<Categories>> GetCategoryByRole(string role)
        {
            var category = await _service.GetByRole(role);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
