using BaseLibrary.DTOs.Patient;
using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.Patient
{
    public class PatientRepository : IPatientInterface
    {
        private readonly AppDbContext _context;
        private readonly JWThelper _jwtHelper;

        public PatientRepository(AppDbContext context, JWThelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile, string token)
        {
            var userId = _jwtHelper.GetUserIdFromToken(token);

            if(string.IsNullOrEmpty(userId) ) return new GeneralResponse(false, "Invalid token");

            var findPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == Convert.ToInt32(userId));
            if (findPatient == null) return new GeneralResponse(false, "User not found");

            // Save patient data
            completeProfile.userId = Convert.ToInt32(userId);
            findPatient.DoB = completeProfile.DoB;
            findPatient.Height = completeProfile.Height;
            findPatient.Weight = completeProfile.Weight;
            findPatient.Gender = completeProfile.Gender;
            findPatient.Is_profile_completed = true;

            // Save patient Allergy
            if (completeProfile.selectedAllergyId.Any())
            {
                var patientAllergies = completeProfile.selectedAllergyId
                .Select(allergyId => new Patient_Allergies
                {
                    PatientId = Convert.ToInt32(userId),
                    AllergyId = allergyId
                }).ToList();

                await _context.Patient_Allergies.AddRangeAsync(patientAllergies);
            }


            // Save patient Medication
            if (completeProfile.selectedMedicationId.Any())
            {
                var patientMedication = completeProfile.selectedMedicationId
                .Select(medicationId => new Patient_Medication
                {
                    PatientId = Convert.ToInt32(userId),
                    MedicationId = medicationId

                }).ToList();
                await _context.patient_Medications.AddRangeAsync(patientMedication);
            }

            // Save patient Disease
            if (completeProfile.selectedDiseaseId.Any())
            {
                var patientDisease = completeProfile.selectedDiseaseId
                    .Select(diseaseId => new Patient_Disease
                    {
                        PatientId = Convert.ToInt32(userId),
                        DiseaseId = diseaseId
                    });
                await _context.Patient_Diseases.AddRangeAsync(patientDisease);
            }
            

            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Profile completed...");

        }

    }
}
