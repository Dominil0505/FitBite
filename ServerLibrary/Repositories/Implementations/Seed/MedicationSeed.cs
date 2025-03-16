using BaseLibrary.Entities;
using BaseLibrary.Responses;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Seed
{
    public class MedicationSeed(AppDbContext _context) : ISeeder
    {
        public async Task<GeneralResponse> SeedAsync()
        {
            await _context.Medications.AddRangeAsync(SeedData.GetMedications());
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "Successfully seed medications");

        }
    }
}