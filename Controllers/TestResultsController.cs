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
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultService _service;

        public TestResultsController(ITestResultService service)
        {
            _service = service;
        }

        [Authorize(Role.Admin, Role.SuperTeacher, Role.Teacher)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestResults>>> GetAll()
        {
            var results = await _service.GetAll();
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<TestResults>> Create(TestResult group)
        {
            group.PassDate = DateTime.Now;
            var newGroupId = await _service.Create(group);
            group.Id = newGroupId;
            return Ok(group);
        }

        /*[HttpGet("GetByСategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<TestResults>>> GetByCategory(int categoryId)
        {
            var tests = await _service.GetByCategory(categoryId);
            return Ok(tests);
        }

        [HttpGet("GetByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<TestResults>>> GetByUser(int userId)
        {
            var tests = await _service.GetByUser(userId);
            return Ok(tests);
        }

        [HttpGet("GetByGroup/{groupId}")]
        public async Task<ActionResult<IEnumerable<TestResults>>> GetByGroup(int groupId)
        {
            var tests = await _service.GetByGroup(groupId);
            return Ok(tests);
        }*/
    }
}
