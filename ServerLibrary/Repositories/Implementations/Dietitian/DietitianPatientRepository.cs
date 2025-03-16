using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Dietitian
{
    public class DietitianPatientRepository(AppDbContext _context) : IDietitian
    {
        public Task<GeneralResponse> AssignMenuToPatient(MenuToPatientDTO menuPatientDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<DietitianPatientPairDTO>> GetPatientDietitianPair(int dietitan_id)
        {
            //var dietitianPatientDTO = _context.Dieticians
            //    .Where(d => d.Dietician_Id == dietitan_id)
            //    .Select(d => new DietitianPatientPairDTO
            //    {
            //        DietitianId = dietitan_id,
            //        PatientId = _context.Patients
            //        .Where(p => p.Dietician_Id == d.Dietician_Id)
            //        .Select(p => p.Patient_Id)
            //    });
            throw new NotImplementedException();
        }
    }
}
