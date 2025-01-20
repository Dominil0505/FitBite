using BaseLibrary.DTOs.AdminFunctionDTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminPatientController(IPatientAssignment<PatientDTO> patientService) : Controller
    {

        [HttpGet("patients")]
        public async Task<IActionResult> GetNewPatientsAsync()
        {
            var result = await patientService.GetPatientAsnyc();
            return Ok(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignToDietitianAsync(PatientDTO AssignPatient)
        {
            var result = await patientService.AssignPatientToDietAsync(AssignPatient);
            return Ok(result);
        }

        [HttpPost("unassign")]
        public async Task<IActionResult> UnassignAsync(int patientId)
        {
            var result = await patientService.UnassignPatientAsync(patientId);
            return Ok(result);
        }

    }
}
