using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class DiseaseRepository(AppDbContext _context) : IGenericRepositoryInterface<Diseases>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);
            if (disease is null) return NotFound();

            _context.Diseases.Remove(disease);
            await Commit();
            return Success();
        }

        public async Task<List<Diseases>> GetAll() => await _context.Diseases.ToListAsync();

        public async Task<Diseases> GetById(int id) => await _context.Diseases.FindAsync(id);

        public async Task<GeneralResponse> Insert(Diseases item)
        {
            if(!await CheckName(item.Disease_Name)) return new GeneralResponse(false, "Allergy is already added");
            _context.Diseases.Add(item);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(Diseases item)
        {
            var disease = await _context.Diseases.FindAsync(item.Disease_Id);
            if (disease is null) return NotFound();

            disease.Disease_Name = item.Disease_Name;
            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, allergies not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Diseases.FirstOrDefaultAsync(_ => _.Disease_Name.ToLower().Equals(name.ToLower()));

            return item is null;
        }
    }
}