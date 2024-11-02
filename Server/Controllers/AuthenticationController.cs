using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register user)
        {
            if (user == null) 
                return BadRequest("Model is empty");
            var result = await accountInterface.CreateUserAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInAsync(CustomLogin user)
        {
            if (user == null)
                return BadRequest("Model is empty");
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsnyc(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is empty");
            var result = await accountInterface.RefreshTokenAsnyc(token);
            return Ok(result);
        }

    }
}
