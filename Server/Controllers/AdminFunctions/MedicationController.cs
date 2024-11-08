using BaseLibrary.DTOs.AdminFunctionDTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController (IGenericRepositoryInterface<MedicationDTO> genericRepositoryInterface) : GenericController<MedicationDTO>(genericRepositoryInterface)
    {
        
    }
}