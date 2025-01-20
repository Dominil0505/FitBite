using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.DTOs.Patient;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController (IPatientInterface patientInterface) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CompleteProfile(CompleteProfileDTO completeProfile, string token)
        {
            if (completeProfile == null) return BadRequest();
            return Ok(await patientInterface.CompleteProfile(completeProfile, token));
        }
    }
}
