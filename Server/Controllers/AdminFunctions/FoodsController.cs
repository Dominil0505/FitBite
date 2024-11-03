using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers.AdminFunctions
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController(IGenericRepositoryInterface<AddFoodsDTO> genericRepositoryInterface) : GenericController<AddFoodsDTO>(genericRepositoryInterface)
    {
    }
}
