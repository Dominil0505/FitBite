using BaseLibrary.DTOs.Profile;
using BaseLibrary.EntitiesRelation;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class ProfileRepository(AppDbContext _context) : IProfile
    {
        /* GET Profiles*/
        public async Task<AdminProfileDTO> getAdminProfile(int user_id)
        {
            var adminDTO = await _context.Users
                .Where(a => a.User_Id == user_id)
                .Select(a => new AdminProfileDTO
                {
                    user_id = a.User_Id,
                    email_address = a.Email,
                    name = a.User_Name,
                    phone_number = a.Mobile
                }).FirstOrDefaultAsync();

            return adminDTO;
        }

        public async Task<DietitianProfileDTO> getDietitianProfile(int user_id)
        {
            var dietitianDTO = await _context.Dieticians
                .Where(d => d.User_Id == user_id)
                .Select(d => new DietitianProfileDTO
                {
                    user_id = user_id,
                    email_address = d.Users.Email,
                    max_patient_number = d.Maximum_Patient_Number,
                    name = d.Users.User_Name,
                    patient_name = _context.Patients
                        .Where(p => p.Dietician_Id == d.Dietician_Id)
                        .Select(p => p.Users.User_Name)
                        .ToList(),
                    patient_number = d.Patient_Number,
                    phone_number = d.Users.Mobile,
                }).FirstOrDefaultAsync();

            return dietitianDTO;

        }

        public async Task<PatientProfileDTO> getPatientProfile(int user_id)
        {
            var patientDTO = await _context.Patients
                .Where(p => p.User_Id == user_id)
                .Select(p => new PatientProfileDTO
                {
                    user_id = user_id,
                    dietitan_name = p.Dieticians != null ? p.Dieticians.Users.User_Name : "No dietitian",
                    email_address = p.Users.Email,
                    name = p.Users.User_Name,
                    gender = p.Gender,
                    height = p.Height,
                    weight = p.Weight,
                    phone_number = p.Users.Mobile,
                    selectedAllergyName = _context.Allergy
                        .Where(a => _context.Patient_Allergies
                            .Where(pa => pa.PatientId == p.Patient_Id)
                            .Select(pa => pa.AllergyId)
                            .Contains(a.Allergy_Id))
                        .Select(a => a.Allergy_Name)
                        .ToList() ?? new List<string>(),
                    selectedDiseaseName = _context.Diseases
                        .Where(d => _context.Patient_Diseases
                            .Where(da => da.PatientId == p.Patient_Id)
                            .Select(da => da.DiseaseId)
                            .Contains(d.Disease_Id))
                        .Select(d => d.Disease_Name)
                        .ToList() ?? new List<string>(),
                    selectedMedicationName = _context.Medications
                        .Where(m => _context.patient_Medications
                            .Where(ma => ma.PatientId == p.Patient_Id)
                            .Select(ma => ma.MedicationId)
                            .Contains(m.Medication_Id))
                        .Select(m => m.Medication_Name)
                        .ToList() ?? new List<string>()

                }).FirstOrDefaultAsync();

            return patientDTO;
        }

        /* UPDATE Profiles*/

        public async Task<GeneralResponse> updatePatientProfile(PatientProfileDTO patientProfile)
        {
            var patient = await _context.Patients
                .Where(_ => _.User_Id == patientProfile.user_id)
                .Include(p => p.Users)
                .Include(p => p.Dieticians.Users)
                .FirstOrDefaultAsync();

            if (patient == null)
                return new GeneralResponse(false, "user not found");

            UpdatePatientDetails(patient, patientProfile);

            await UpdatePatientRelations(
                patient.Patient_Id,
                patientProfile.selectedAllergyName,
                _context.Patient_Allergies,
                _context.Allergy,
                (allergy, patientId) => new Patient_Allergies { AllergyId = allergy.Allergy_Id, PatientId = patientId }
            );

            await UpdatePatientRelations(
                patient.Patient_Id,
                patientProfile.selectedDiseaseName,
                _context.Patient_Diseases,
                _context.Diseases,
                (disease, patientId) => new Patient_Disease { DiseaseId = disease.Disease_Id, PatientId = patientId }
            );

            await UpdatePatientRelations(
                patient.Patient_Id,
                patientProfile.selectedMedicationName,
                _context.patient_Medications,
                _context.Medications,
                (medication, patientId) => new Patient_Medication { MedicationId = medication.Medication_Id, PatientId = patientId }
            );

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> updateAdminProfile(AdminProfileDTO adminProfile)
        {
            var admin = await _context.Users.FirstOrDefaultAsync(_ => _.User_Id == adminProfile.user_id);
            if (admin == null)
                return new GeneralResponse(false, "Admin not found");

            admin.Mobile = adminProfile.phone_number;

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> updateDietitianprofile(DietitianProfileDTO dietitianProfile)
        {
            var dietitian = await _context.Dieticians
                .Where(_ => _.User_Id == dietitianProfile.user_id)
                .Include(u => u.Users)
                .FirstOrDefaultAsync();

            dietitian.Maximum_Patient_Number = dietitianProfile.max_patient_number;
            dietitian.Patient_Number = dietitianProfile.patient_number;
            dietitian.Users.Mobile = dietitianProfile.phone_number;

            await Commit();
            return Success();
        }

        /* DELETE Profiles*/

        public async Task<GeneralResponse> deleteAdminProfile(int user_id)
        {
            var findUser = await _context.Users.FindAsync(user_id);
            var findAdmin = await _context.Admins.FirstOrDefaultAsync(_ => _.User_Id == user_id);
            var findUserRole = await _context.User_Roles.FirstOrDefaultAsync(_ =>_.UserId == user_id);

            if (findUser == null) return new GeneralResponse(false, "User not found");
            if (findAdmin == null) return new GeneralResponse(false, "Admin not found");
            if (findUserRole == null) return new GeneralResponse(false, "Something went wrong at user_role");

            _context.Users.Remove(findUser);
            _context.Admins.Remove(findAdmin);
            _context.User_Roles.Remove(findUserRole);
            await Commit();

            return Success();
        }

        public async Task<GeneralResponse> deleteDietitianProfile(int user_id)
        {
            var findUser = await _context.Users.FindAsync(user_id);
            var findDietitian = await _context.Dieticians.FirstOrDefaultAsync(_ => _.User_Id == user_id);
            var findDietitianPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == user_id);
            var findUserRole = await _context.User_Roles.FirstOrDefaultAsync(_ => _.UserId == user_id);

            if (findDietitianPatient != null)
            {
                findDietitianPatient.Dietician_Id = null;
            }
            if (findDietitian == null) return new GeneralResponse(false, "Dietitian not found");
            if (findUserRole == null) return new GeneralResponse(false, "Something went wrong at user_role");

            _context.Users.Remove(findUser);
            _context.Dieticians.Remove(findDietitian);
            _context.User_Roles.Remove(findUserRole);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> deletePatientProfile(int user_id)
        {
            var findUser = await _context.Users.FindAsync(user_id);
            var findPatient = await _context.Patients.FirstOrDefaultAsync(_ => _.User_Id == findUser.User_Id);
            var findUserRole = await _context.User_Roles.FirstOrDefaultAsync(_ => _.UserId == user_id);

            if (findUser == null) return new GeneralResponse(false, "User not found");
            if (findPatient == null) return new GeneralResponse(false, "Patient not found");
            if (findUserRole == null) return new GeneralResponse(false, "Something went wrong at user_role");

            var patientAllergy = await _context.Patient_Allergies.FirstOrDefaultAsync(_ => _.PatientId == findPatient.Patient_Id);

            if(patientAllergy != null)
            {
                _context.Patient_Allergies.Remove(patientAllergy);
            }
            
            var patientDisease = await _context.Patient_Diseases.FirstOrDefaultAsync(_ => _.PatientId == findPatient.Patient_Id);

            if(patientDisease != null) 
            {  
                _context.Patient_Diseases.Remove(patientDisease); 
            }

            var patientMedication = await _context.patient_Medications.FirstOrDefaultAsync(_ => _.PatientId == findPatient.Patient_Id);

            if(patientMedication != null)
            {
                _context.patient_Medications.Remove(patientMedication);
            }

            _context.User_Roles.Remove(findUserRole);
            _context.Users.Remove(findUser);
            _context.Patients.Remove(findPatient);

            await Commit();
            return Success();
        }


        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private void UpdatePatientDetails(Patients patient, PatientProfileDTO profile)
        {
            patient.Weight = profile.weight;
            patient.Height = profile.height;
            patient.Users.Mobile = profile.phone_number;
        }

        private async Task UpdatePatientRelations<TPatientRelation, TReference>(
            int patient_id,
            IEnumerable<string>? selectedName,
            DbSet<TPatientRelation> patientRelationSet,
            DbSet<TReference> referenceSet,
            Func<TReference, int, TPatientRelation> createRelation
            ) 
            where TPatientRelation : class
            where TReference : class
            {
            if (selectedName?.Any() != true)
                return;

            // Delete current relation
            var existingRelations = await patientRelationSet
                .Where(_ => EF.Property<int>(_, "PatientId") == patient_id)
                .ToListAsync();

            patientRelationSet.RemoveRange(existingRelations);

            // Create new relations
            IQueryable<TReference> query = referenceSet;

            if(typeof(TReference) == typeof(Allergies))
            {
                query = query.Where(r => selectedName.Contains(EF.Property<string>(r, "Allergy_Name")));
            }
            else if(typeof(TReference) == typeof(Medication))
            {
                query = query.Where(r => selectedName.Contains(EF.Property<string>(r, "Medication_Name")));
            }
            else if(typeof(TReference) == typeof(Diseases))
            {
                query = query.Where(r => selectedName.Contains(EF.Property<string>(r, "Disease_Name")));
            }
            else
            {
                throw new InvalidOperationException($"Unspupported reference type: {typeof(TReference).Name}");
            }


            var references = await query.ToListAsync();
            var newRelations = references.Select(r => createRelation(r, patient_id)).ToList();
            patientRelationSet.AddRange(newRelations);
        }
    }
}
