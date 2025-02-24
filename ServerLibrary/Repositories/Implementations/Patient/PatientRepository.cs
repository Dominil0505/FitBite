using BaseLibrary.DTOs.Patient;
using BaseLibrary.EntitiesRelation;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Linq.Expressions;

namespace ServerLibrary.Repositories.Implementations.Patient
{
    public class PatientRepository : IPatientInterface
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile)
        {

            if(completeProfile.userId == null) return new GeneralResponse(false, "User not found");

            var findPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == completeProfile.userId);
            if (findPatient == null) return new GeneralResponse(false, "Patient not found");

            // Save patient data
            findPatient.DoB = completeProfile.DoB;
            findPatient.Height = completeProfile.Height;
            findPatient.Weight = completeProfile.Weight;
            findPatient.Gender = completeProfile.Gender;
            findPatient.Is_profile_completed = true;

            var searchPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == completeProfile.userId);
            // Allergies mentése
            if (completeProfile.selectedAllergyName != null && completeProfile.selectedAllergyName.Any())
            {
                foreach (var selectedAllergy in completeProfile.selectedAllergyName)
                {
                    var searchAllergy = await _context.Allergy.FirstOrDefaultAsync(_ => _.Allergy_Name == selectedAllergy);
                    

                    if(searchAllergy != null)
                    {
                        _context.Patient_Allergies.Add(new Patient_Allergies
                        {
                            AllergyId = searchAllergy.Allergy_Id,
                            PatientId = searchPatient!.Patient_Id
                        });
                    }
                }
            }

            if(completeProfile.selectedDiseaseName != null && completeProfile.selectedDiseaseName.Any())
            {
                foreach (var selectedDisease in completeProfile.selectedDiseaseName)
                {
                    var searchDisease = await _context.Diseases.FirstOrDefaultAsync(_ => _.Disease_Name == selectedDisease);

                    if( searchDisease != null)
                    {
                        _context.Patient_Diseases.Add(new Patient_Disease
                        {
                            DiseaseId = searchDisease.Disease_Id,
                            PatientId = searchPatient!.Patient_Id
                        });
                    }
                }
            }

            if(completeProfile.selectedMedicationName != null && completeProfile.selectedMedicationName.Any())
            {
                foreach (var selectedMedication in completeProfile.selectedMedicationName)
                {
                    var searchMedication = await _context.Medications.FirstOrDefaultAsync(_ => _.Medication_Name == selectedMedication);

                    if(searchMedication != null)
                    {
                        _context.patient_Medications.Add(new Patient_Medication
                        {
                            MedicationId = searchMedication.Medication_Id,
                            PatientId = searchPatient!.Patient_Id
                        });
                    }
                }
            }

            await _context.SaveChangesAsync();
            return new GeneralResponse(true, "Profile successfully completed");

        }

        public async Task<GeneralResponse> IsProfileCompleted(int user_id)
        {
            var findPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == user_id);
            if(findPatient == null)
            {
                return new GeneralResponse(false, "Patient not found.");
            }

            if(findPatient.Is_profile_completed == false) 
            {
                return new GeneralResponse(false, "Profile is not completed.");
            }

            return new GeneralResponse(true, "Profile completed.");
        }
        
    }
}
