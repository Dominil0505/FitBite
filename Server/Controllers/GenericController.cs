using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<T>(IGenericRepositoryInterface<T> genericRepositoryInterface) : Controller where T : class
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll() => Ok(await genericRepositoryInterface.GetAll());

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.DeleteById(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if(model is null) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.Insert(model));
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            if(model is null) return BadRequest("Invalid request sent");
            return Ok(await genericRepositoryInterface.Update(model));
        }
    }
}