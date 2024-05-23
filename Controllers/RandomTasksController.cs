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
    public class RandomTasksController : ControllerBase
    {
        private readonly IRandomTaskService _service;

        public RandomTasksController(IRandomTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RandomTasks>>> GetAll()
        {
            var tasks = await _service.GetAll();
            return Ok(tasks);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPost]
        public async Task<ActionResult<RandomTasks>> Create(RandomTask task)
        {
            var newGroupId = await _service.Create(task);
            task.Id = newGroupId;
            return CreatedAtAction(nameof(GetById), new { id = newGroupId }, task);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RandomTask task)
        {
            var existingTask = await _service.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            task.Id = id;
            if (await _service.Update(id, task))
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, task);
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTask = await _service.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            if (await _service.Delete(id))
            {
                return Ok(new { message = "Task deleted successfully" });
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RandomTasks>> GetById(int id)
        {
            var task = await _service.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
    }
}
