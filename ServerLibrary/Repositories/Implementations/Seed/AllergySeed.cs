using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Responses;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Seed
{
    public class AllergySeed(AppDbContext _context) : ISeeder
    {
        public async Task<GeneralResponse> SeedAsync()
        {
            await _context.Allergy.AddRangeAsync(SeedData.GetAllergies());
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Successfully seed allergy");
        }
    }
}