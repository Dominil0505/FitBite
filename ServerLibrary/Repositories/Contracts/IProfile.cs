﻿using BaseLibrary.DTOs.Profile;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IProfile
    {
        Task<List<PatientProfileDTO>> getPatientProfile(int user_id); 
        Task<List<AdminProfileDTO>> getAdminProfile(int user_id);
        Task<List<DietitianProfileDTO>> getDietitianProfile(int user_id);

        Task<GeneralResponse> updatePatientProfile(PatientProfileDTO patientProfile);
        Task<GeneralResponse> deletePatientProfile(int user_id);

        Task<GeneralResponse> updateAdminProfile(AdminProfileDTO adminProfile);
        Task<GeneralResponse> deleteAdminProfile(int user_id);

        Task<GeneralResponse> updateDietitianprofile(DietitianProfileDTO dietitianProfile);
        Task<GeneralResponse> deleteDietitianProfile(int user_id);
    }
}
