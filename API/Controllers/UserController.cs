using Application.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await userService.GetUser());
        }
    }
}
