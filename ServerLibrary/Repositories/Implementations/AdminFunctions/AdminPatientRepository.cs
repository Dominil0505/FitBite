using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class AdminPatientRepository(AppDbContext _context) : IPatientAssignment<PatientDTO>
    {
        public async Task<List<PatientDTO>> GetPatientAsnyc()
        {
            var patientsWithDieticians = await _context.Patients
                .Include(p => p.Users)
                .Include(p => p.Dieticians.Users)
                .ToListAsync();

            var patientDtoList = patientsWithDieticians.Select(p => new PatientDTO
            {
                Patient_Id = p.Patient_Id,
                Patient_Name = p.Users.User_Name,
                Patient_Email = p.Users.Email,
                Dietitian_Id = p.Dietician_Id,
                Dietitian_Name = p.Dieticians?.Users?.User_Name ?? "No Dietitian",
                IsProfileCompleted = p.Is_profile_completed ?? false,
                Status = (p.Dietician_Id != null) ? "Completed" : "New"
            }).ToList();

            return patientDtoList;
        }


        public async Task<GeneralResponse> AssignPatientToDietAsync(PatientDTO AssignPatient)
        {
            NotFoundPatient((int)AssignPatient.Patient_Id!);
            if (AssignPatient.Dietitian_Id <= 0 || AssignPatient.Dietitian_Id == null) return new GeneralResponse(false, "Dietitan not found,No such a dietitan id");


            var getPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.Patient_Id == AssignPatient.Patient_Id);
            var getDietitan = await _context.Dieticians.FirstOrDefaultAsync(_ => _.Dietician_Id == AssignPatient.Dietitian_Id);

            if (getPatient == null) return new GeneralResponse(false, "Patient not found, does not exist in the database");
            if (getDietitan == null) return NotFoundDietitan();

            getPatient.Dietician_Id = AssignPatient.Dietitian_Id;
            getPatient.Assign_Date = DateTime.Now;
            await Commit();
            return Success();

        }

        public async Task<GeneralResponse> UnassignPatientAsync(int patientId)
        {
            NotFoundPatient(patientId);

            var getPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.Patient_Id == patientId);
            getPatient.Dietician_Id = null;
            getPatient.Assign_Delete_Date = DateTime.Now;
            await Commit();
            return Success();

        }

        private static GeneralResponse NotFoundPatient(int patientId)
        {
            if (patientId <= 0 || patientId == null) return new GeneralResponse(false, "Patient not found, ID does not exist");
            else return new GeneralResponse(false); ;
        }

        private static GeneralResponse NotFoundDietitan() => new(false, "Dietitian not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

    }
}
