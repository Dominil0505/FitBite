using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController (IGenericRepositoryInterface<Diseases> genericRepositoryInterface) : GenericController<Diseases>(genericRepositoryInterface)
    {
        
    }
}