using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;


namespace ServerLibrary.Repositories.Implementations
{
    public class RolesRepository(AppDbContext _context) : IUserRoles
    {
        public async Task<List<Roles>> GetAllRoles() => await _context.Roles.ToListAsync();
    }
}
