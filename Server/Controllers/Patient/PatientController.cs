using BaseLibrary.DTOs.Patient;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController (IPatient patientInterface) : Controller
    {
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(CompleteProfileDTO completeProfile)
        {
            if (completeProfile == null) return BadRequest();
            return Ok(await patientInterface.CompleteProfile(completeProfile));
        }

        [HttpGet("is-profile-completed/{user_id}")]
        public async Task<IActionResult> IsProfileCompleted(int user_id)
        {
            if(user_id == null) return BadRequest();
            return Ok(await patientInterface.IsProfileCompleted(user_id));
        }
    }
}
