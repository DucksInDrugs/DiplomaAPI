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
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _service;

        public VideoController(IVideoService service)
        {
            _service = service;
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Videos>>> GetAllVideos()
        {
            var videos = await _service.GetAll();
            return Ok(videos);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Videos>> GetVideoById(int id)
        {
            var test = await _service.GetById(id);
            if (test == null)
            {
                return NotFound();
            }
            return Ok(test);
        }

        [HttpGet("GetByСategory/{categoryId}")]
        public async Task<ActionResult<Videos>> GetVideoByCategory(int categoryId)
        {
            var videos = await _service.GetByCategory(categoryId);
            return Ok(videos);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPost]
        public async Task<ActionResult<Videos>> CreateVideo(Video video)
        {
            var newVideoId = await _service.Create(video);
            video.Id = newVideoId;
            return CreatedAtAction(nameof(GetVideoById), new { id = newVideoId }, video);
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, Video video)
        {
            var existingVideo = await _service.GetById(id);
            if (existingVideo == null)
            {
                return NotFound();
            }

            video.Id = id;
            if (await _service.Update(id, video))
            {
                return CreatedAtAction(nameof(GetVideoById), new { id = id }, video);
            }
            return StatusCode(500, "Internal server error");
        }

        [Authorize(Role.Admin, Role.SuperTeacher)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var existingVideo = await _service.GetById(id);
            if (existingVideo == null)
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
