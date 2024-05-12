using DiplomaAPI.Entities;
using DiplomaAPI.Models;
using DiplomaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedTasksController : ControllerBase
    {
        private readonly ICompletedTaskService _service;

        public CompletedTasksController(ICompletedTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedTasks>>> GetAllCompletedTasks()
        {
            var tasks = await _service.GetAll();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<CompletedTasks>> CreateCompletedTask(CompletedTask task)
        {
            var newTaskId = await _service.Create(task);
            task.Id = newTaskId;
            return Ok(task);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProgress(int userId)
        {
            if (await _service.Delete(userId))
            {
                return Ok(new { message = "Tasks deleted successfully" });
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpGet("TasksByIds/{userId}/{categoryId}")]
        public async Task<ActionResult<Categories>> GetByIds(int userId, int categoryId)
        {
            var task = await _service.GetByIds(userId, categoryId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}