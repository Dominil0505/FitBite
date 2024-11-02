using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class MedicationRepository(AppDbContext _context) : IGenericRepositoryInterface<Medication>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication is null) return NotFound();

            _context.Medications.Remove(medication);
            await Commit();
            return Success();
        }

        public async Task<List<Medication>> GetAll() => await _context.Medications.ToListAsync();

        public async Task<Medication> GetById(int id) => await _context.Medications.FindAsync(id);

        public async Task<GeneralResponse> Insert(Medication item)
        {
            if(!await CheckName(item.Medication_Name)) return new GeneralResponse(false, "Allergy is already added");
            _context.Medications.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Medication item)
        {
            var medication = await _context.Medications.FindAsync(item.Medication_Id);
            if (medication is null) return NotFound();

            medication.Medication_Name = item.Medication_Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, allergies not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Medications.FirstOrDefaultAsync(_ => _.Medication_Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}