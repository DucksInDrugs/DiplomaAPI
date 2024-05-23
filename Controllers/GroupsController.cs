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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _service;

        public GroupsController(IGroupService service)
        {
            _service = service;
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Groups>>> GetAll()
        {
            var groups = await _service.GetAll();
            return Ok(groups);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPost]
        public async Task<ActionResult<Groups>> Create(Group group)
        {
            var newGroupId = await _service.Create(group);
            group.Id = newGroupId;
            return CreatedAtAction(nameof(GetById), new { id = newGroupId }, group);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Group group)
        {
            var existingGroup = await _service.GetById(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            group.Id = id;
            if (await _service.Update(id, group))
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, group);
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGroup = await _service.GetById(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            if (await _service.Delete(id))
            {
                return Ok(new { message = "Group deleted successfully" });
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Groups>> GetById(int id)
        {
            var group = await _service.GetById(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }
    }
}
