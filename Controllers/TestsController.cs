using DiplomaAPI.Authorization;
using DiplomaAPI.Entities;
using DiplomaAPI.Models;
using DiplomaAPI.Services;
using DiplomaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _service;

        public TestsController(ITestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tests>>> GetAllTests()
        {
            var tests = await _service.GetAll();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tests>> GetTestById(int id)
        {
            var test = await _service.GetById(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        [HttpGet("GetByСategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Tests>>> GetTestsByCategory(int categoryId)
        {
            var tests = await _service.GetByCategory(categoryId);
            return Ok(tests);
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<Tests>> CreateTest(Test test)
        {
            var newTestId = await _service.Create(test);
            test.Id = newTestId;
            return CreatedAtAction(nameof(GetTestById), new { id = newTestId }, test);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTest(int id, Test test)
        {
            var existingTest = await _service.GetById(id);
            if (existingTest == null)
            {
                return CreatedAtAction(nameof(GetTestById), new { id = id }, test);
            }

            test.Id = id;
            if (await _service.Update(id, test))
            {
                return NoContent();
            }
            return StatusCode(500, "Internal server error");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var existingTest = await _service.GetById(id);
            if (existingTest == null)
            {
                return NotFound();
            }

            if (await _service.Delete(id))
            {
                return NoContent();
            }
            return StatusCode(500, "Internal server error");
        }
    }
}
