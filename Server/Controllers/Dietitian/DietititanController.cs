using BaseLibrary.DTOs.Dietitian;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.Dietitian
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietititanController(IAvailableDietitianInterface<AvailableDietDTO> dietitanService) : ControllerBase
    {
        [HttpGet("available-dietitians")]
        public async Task<IActionResult> GetAvailableDietitans()
        {
            var result = await dietitanService.GetAvailableDietitans();
            return Ok(result);
        }
    }
}
