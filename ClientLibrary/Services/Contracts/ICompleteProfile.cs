using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface ICompleteProfile
    {
        Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile, string baseUrl);
        Task<GeneralResponse> IsProfileCompleted(int user_id, string baseUrl);
    }
}
