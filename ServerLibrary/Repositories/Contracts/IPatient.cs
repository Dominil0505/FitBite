using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IPatient
    {
        Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile);
        Task<GeneralResponse> IsProfileCompleted(int user_id);
    }
}
