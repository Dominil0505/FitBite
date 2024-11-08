using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class MedicationRepository(AppDbContext _context) : IGenericRepositoryInterface<MedicationDTO>
    {
        public async Task<List<MedicationDTO>> GetAll()
        {
            var medications = await _context.Medications.ToListAsync();

            var medicationDTOList = medications.Select(m => new MedicationDTO
            {
                Medication_Id = m.Medication_Id,
                Medication_Name = m.Medication_Name

            }).ToList();

            return medicationDTOList;
        }

        public async Task<GeneralResponse> Insert(MedicationDTO item)
        {
            if (!await CheckName(item.Medication_Name)) return new GeneralResponse(false, "Medication is already added");

            var newMedication = new Medication
            {
                Medication_Name = item.Medication_Name,
            };

            _context.Medications.Add(newMedication);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(MedicationDTO item)
        {
            var medication = await _context.Medications.FindAsync(item.Medication_Id);
            if (medication is null) return NotFound();

            medication.Medication_Name = item.Medication_Name;
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication is null) return NotFound();

            _context.Medications.Remove(medication);
            await Commit();
            return Success();
        }

        public async Task<MedicationDTO> GetById(int id) 
        {
            var medication = await _context.Medications.FindAsync(id);

            var medicationDTO = new MedicationDTO
            {
                Medication_Id = medication.Medication_Id,
                Medication_Name = medication.Medication_Name
            };

            return medicationDTO;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Medication not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Medications.FirstOrDefaultAsync(_ => _.Medication_Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}