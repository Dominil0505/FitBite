using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class RolesRepository(AppDbContext _context) : IUserRoles<Roles>
    {
        public async Task<List<Roles>> GetAllRoles() => await _context.Roles.ToListAsync();
    }
}
