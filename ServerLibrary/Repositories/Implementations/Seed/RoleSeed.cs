using BaseLibrary.Responses;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Seed
{
    public class RoleSeed(AppDbContext _context) : ISeeder
    {
        public async Task<GeneralResponse> SeedAsync()
        {
            await _context.Roles.AddRangeAsync(SeedData.GetRoles());
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Successfully seed roles");
        }
    }
}