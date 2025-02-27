using BaseLibrary.DTOs.Profile;
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

        [HttpPut("update-admin-profile")]
        public async Task<IActionResult> updateAdminProfile(AdminProfileDTO profile)
        {
            if (profile.user_id == null) return BadRequest();

            var result = await profileInterface.updateAdminProfile(profile);
            return Ok(result);
        }

        [HttpPut("update-dietitian-profile")]
        public async Task<IActionResult> updateDietitianProfile(DietitianProfileDTO profile)
        {
            if (profile.user_id == null) return BadRequest();

            var result = await profileInterface.updateDietitianprofile(profile);
            return Ok(result);
        }

        [HttpPut("update-patient-profile")]
        public async Task<IActionResult> updatePatientProfile(PatientProfileDTO profile)
        {
            if (profile.user_id == null) return BadRequest();

            var result = await profileInterface.updatePatientProfile(profile);
            return Ok(result);
        }
    }
}
