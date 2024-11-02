using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class AllergyRepository(AppDbContext _context) : IGenericRepositoryInterface<Allergies>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var allergy = await _context.Allergy.FindAsync(id);
            if (allergy is null) return NotFound();

            _context.Allergy.Remove(allergy);
            await Commit();
            return Success();
        }

        public async Task<List<Allergies>> GetAll() => await _context.Allergy.ToListAsync();

        public async Task<Allergies> GetById(int id) => await _context.Allergy.FindAsync(id);

        public async Task<GeneralResponse> Insert(Allergies item)
        {
            if(!await CheckName(item.Allergy_Name)) return new GeneralResponse(false, "Allergy is already added");
            _context.Allergy.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Allergies item)
        {
            var allergy = await _context.Allergy.FindAsync(item.Allergy_Id);
            if (allergy is null) return NotFound();

            allergy.Allergy_Name = item.Allergy_Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, allergies not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Allergy.FirstOrDefaultAsync(_ => _.Allergy_Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}