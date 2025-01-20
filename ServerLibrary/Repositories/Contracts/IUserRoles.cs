using BaseLibrary.Entities;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserRoles
    {
        Task<List<Roles>> GetAllRoles();
    }
}
