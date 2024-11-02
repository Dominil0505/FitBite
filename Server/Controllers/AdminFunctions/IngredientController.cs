using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController (IGenericRepositoryInterface<Ingredient> genericRepositoryInterface) : GenericController<Ingredient>(genericRepositoryInterface)
    {
        
    }
}