using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    public class ProfileController(IProfile profileInterface) : Controller
    {
        [HttpGet("get-patient-profile/{user_id}")]
        public async Task<IActionResult> getPatientProfile(int user_id)
        {
            if (user_id == null) return BadRequest();
            var result = await profileInterface.getPatientProfile(user_id);
            return Ok(result);
        }

        [HttpGet("get-dietitian-profile/{user_id}")]
        public async Task<IActionResult> getDietitianProfile(int user_id)
        {
            if(user_id == null) return BadRequest(); 

            var result = await profileInterface.getDietitianProfile(user_id);
            return Ok(result);
        }

        [HttpGet("get-admin-profile/{user_id}")]
        public async Task<IActionResult> getAdminProfile(int user_id)
        {
            if (user_id == null) return BadRequest();

            var result = await profileInterface.getAdminProfile(user_id);
            return Ok(result);
        }

        [HttpDelete("delete-patient-profile")]
        public async Task<IActionResult> deletePatientProfile(int user_id)
        {
            if (user_id == null) return BadRequest();
            
            var result = await profileInterface.deletePatientProfile(user_id);
            return Ok(result);
        }

        [HttpDelete("delete-dietitian-profile")]
        public async Task<IActionResult> deleteDietitianProfile(int user_id)
        {
            if (user_id == null) return BadRequest();

            var result = await profileInterface.deleteDietitianProfile(user_id);
            return Ok(result);
        }

        [HttpDelete("delete-admin-profile")]
        public async Task<IActionResult> deleteAdminProfile(int user_id)
        {
            if (user_id == null) return BadRequest();

            var result = await profileInterface.deleteAdminProfile(user_id);
            return Ok(result);
        }
    }
}
