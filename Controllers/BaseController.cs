using DiplomaAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
