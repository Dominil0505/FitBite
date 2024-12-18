using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.DTOs.Patient;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Data.Migrations;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Patient
{
    public class PatientRepository(AppDbContext _context) : IPatientAssignment<PatientDTO>
    {
        public async Task<GeneralResponse> AssignPatientToDietAsync(int patientId, int dietitanId)
        {
            NotFoundPatient(patientId);
            if (dietitanId <= 0) return new GeneralResponse(false, "No such a dietitan id");


            var getPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.Patient_Id == patientId);
            var getDietitan = await _context.Dieticians.FirstOrDefaultAsync(_ => _.Dietician_Id == dietitanId);

            if (getPatient == null) return new GeneralResponse(false, "Patient is not found");
            if (getDietitan == null) return NotFoundDietitan();

            getPatient.Dietician_Id = dietitanId;
            await Commit();
            return Success();

        }

        public async Task<List<PatientDTO>> GetNewlyRegisteredPatientAsnyc()
        {
            var newPatients = await _context.Patients
            .Where(_ => _.Dietician_Id == null)
            .Include(u => u.Users)
            .ToListAsync();

            var patientDtoList = newPatients.Select(p => new PatientDTO
            {
                Patient_Id = p.Patient_Id,
                Patient_Name = p.Users.User_Name,
                Patient_Email = p.Users.Email,
                IsProfileCompleted = (bool)p.Is_profile_completed
            }).ToList();

            return patientDtoList;
        }

        public async Task<GeneralResponse> UnassignPatientAsync(int patientId)
        {
            NotFoundPatient(patientId);

            var getPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.Patient_Id == patientId);
            getPatient.Dietician_Id = null;
            await Commit();
            return Success();

        }

        public async Task<List<PatientDTO>> GetPatientDietianPair()
        {
            var patientsWithDieticians = await _context.Patients
                .Where(p => p.Dietician_Id != null)
                .Include(p => p.Users)
                .Include(p => p.Dieticians.Users)
                .ToListAsync();

            var patientDtoList = patientsWithDieticians.Select(p => new PatientDTO
            {
                Patient_Id = p.Patient_Id,
                Patient_Name = p.Users.User_Name,
                Patient_Email = p.Users.Email,
                Dietitan_Id = p.Dietician_Id,
                Dietitan_Name = p.Dieticians?.Users?.User_Name ?? "No Dietitian",
                IsProfileCompleted = p.Is_profile_completed ?? false
            }).ToList();

            return patientDtoList;
        }

        public async Task<GeneralResponse> CompleteProfile(int userId, CompleteProfileDTO completeProfileDTO)
        {
            var findPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == userId);

            NotFoundPatient(findPatient.Patient_Id);

            if (findPatient.Is_profile_completed == true) return new GeneralResponse(false, "Profile is completed");

            var updatePatient = new Patients
            {
                DoB = completeProfileDTO.DoB,
                Weight = completeProfileDTO.Weight,
                Height = completeProfileDTO.Height,
                Gender = completeProfileDTO.Gender,
            };

            await _context.SaveChangesAsync();
            return Success();
        }

        private static GeneralResponse NotFoundPatient(int patientId)
        {
            if (patientId <= 0 || patientId == null) return new GeneralResponse(false, "Sorry, patient not found");
            else return new GeneralResponse(false); ;
        }

        private static GeneralResponse NotFoundDietitan() => new(false, "Sorry, dietitian not found");
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

    }
}
