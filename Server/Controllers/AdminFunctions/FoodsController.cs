using BaseLibrary.DTOs.AdminFunctionDTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController(IGenericRepositoryInterface<FoodsDTO> genericRepositoryInterface) : ControllerBase
    {

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() => Ok(await genericRepositoryInterface.GetAll());


        [HttpPost("add")]
        public async Task<IActionResult> Insert(FoodsDTO item)
        {
            if(item is null) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.Insert(item));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(FoodsDTO item)
        {
            if(item is null) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.Update(item));
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (id <= 0) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.DeleteById(id));
        }

    }
}
