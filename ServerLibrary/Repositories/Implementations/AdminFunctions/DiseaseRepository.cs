using BaseLibrary.Entities;
using BaseLibrary.Responses;
using BaseLibrary.DTOs.AdminFunctionDTOs;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class DiseaseRepository(AppDbContext _context) : IGenericRepositoryInterface<DiseaseDTO>
    {
        public async Task<List<DiseaseDTO>> GetAll()
        {
            var diseases = await _context.Diseases.ToListAsync();

            var diseaseDTOList = diseases.Select(d => new DiseaseDTO
            {
                Disease_Id = d.Disease_Id,
                Disease_Name = d.Disease_Name
            }).ToList();

            return diseaseDTOList;
        }

        public async Task<GeneralResponse> Insert(DiseaseDTO item)
        {
            if (!await CheckName(item.Disease_Name)) return new GeneralResponse(false, "Disease is already added");

            var newDisease = new Diseases
            {
                Disease_Name = item.Disease_Name,
            };

            _context.Diseases.Add(newDisease);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(DiseaseDTO item)
        {
            var disease = await _context.Diseases.FindAsync(item.Disease_Id);
            if (disease is null) return NotFound();

            disease.Disease_Name = item.Disease_Name;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);
            if (disease is null) return NotFound();

            _context.Diseases.Remove(disease);
            await Commit();
            return Success();
        }

        public async Task<DiseaseDTO> GetById(int id) 
        {
            var disease = await _context.Diseases.FindAsync(id);

            var diseaseDTO = new DiseaseDTO
            {
                Disease_Id = disease.Disease_Id,
                Disease_Name = disease.Disease_Name
            };

            return diseaseDTO;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, disease not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Diseases.FirstOrDefaultAsync(_ => _.Disease_Name.ToLower().Equals(name.ToLower()));

            return item is null;
        }
    }
}