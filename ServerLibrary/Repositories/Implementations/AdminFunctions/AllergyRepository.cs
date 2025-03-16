using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.DTOs.AdminFunctionDTOs;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class AllergyRepository(AppDbContext _context) : IGenericRepository<AllergyDTO>
    {
        public async Task<List<AllergyDTO>> GetAll()
        {
            var allergies = await _context.Allergy.ToListAsync();

            var allergyDTOList = allergies.Select(a => new AllergyDTO
            {
                Allergy_Id = a.Allergy_Id,
                Allergy_Name = a.Allergy_Name,
            }).ToList();

            return allergyDTOList;
        }

        public async Task<GeneralResponse> Insert(AllergyDTO item)
        {
            if (!await CheckName(item.Allergy_Name)) return new GeneralResponse(false, "Allergy is already added");

            var newAllergy = new Allergies
            {
                Allergy_Name = item.Allergy_Name,
            };

            _context.Allergy.Add(newAllergy);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var allergy = await _context.Allergy.FindAsync(id);
            if (allergy is null) return NotFound();

            _context.Allergy.Remove(allergy);
            await Commit();
            return Success();
        }


        public async Task<AllergyDTO> GetById(int id) {     
            var allergy = await _context.Allergy.FindAsync(id);

            var allergyDTO = new AllergyDTO 
            {
                Allergy_Id = allergy.Allergy_Id,
                Allergy_Name = allergy.Allergy_Name,
            };

            return allergyDTO;
        }

        public async Task<GeneralResponse> Update(AllergyDTO item)
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