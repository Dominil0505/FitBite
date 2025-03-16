using BaseLibrary.Responses;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Seed
{
    public class DiseaseSeed(AppDbContext _context) : ISeeder
    {
        public async Task<GeneralResponse> SeedAsync()
        {
            await _context.Diseases.AddRangeAsync(SeedData.GetDiseases());

            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Successfully seed disease");
        }
    }
}