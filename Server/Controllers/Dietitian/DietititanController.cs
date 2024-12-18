using BaseLibrary.DTOs.Dietitian;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.Dietitian
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietititanController(IDietitianInterface<AvailableDietDTO> dietitanService) : ControllerBase
    {
        [HttpGet("AvailableDiet")]
        public async Task<IActionResult> GetAvailableDietitans()
        {
            var result = await dietitanService.GetAvailableDietitans();
            return Ok(result);
        }
    }
}
