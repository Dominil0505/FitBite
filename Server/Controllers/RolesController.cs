﻿using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(IUserRoles rolesInterface) : ControllerBase
    {
        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            var result = await rolesInterface.GetAllRoles();
            return Ok(result);
                
        }
    }
}
