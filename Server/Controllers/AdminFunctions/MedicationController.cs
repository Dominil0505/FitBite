using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController (IGenericRepositoryInterface<Medication> genericRepositoryInterface) : GenericController<Medication>(genericRepositoryInterface)
    {
        
    }
}