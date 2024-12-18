using BaseLibrary.DTOs.AdminFunctionDTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.Patient
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController(IPatientAssignment<PatientDTO> patientService) : Controller
    {

        [HttpGet("NewPatients")]
        public async Task<IActionResult> GetNewPatientsAsync()
        {
            var result = await patientService.GetNewlyRegisteredPatientAsnyc();
            return Ok(result);
        }

        [HttpPost("AssignToDietitian")]
        public async Task<IActionResult> AssignToDietitianAsync(int patientId, int dietitianId)
        {
            var result = await patientService.AssignPatientToDietAsync(patientId, dietitianId);
            return Ok(result);
        }

        [HttpPost("Unassign")]
        public async Task<IActionResult> UnassignAsync(int patientId)
        {
            var result = await patientService.UnassignPatientAsync(patientId);
            return Ok(result);
        }

        [HttpGet("DietPatientPair")]
        public async Task<IActionResult> GetPatientDietianPair()
        {
            var result = await patientService.GetPatientDietianPair();
            return Ok(result);
        }

    }
}
