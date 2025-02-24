using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IPatientInterface
    {
        Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile);
        Task<GeneralResponse> IsProfileCompleted(int user_id);
    }
}
