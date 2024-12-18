using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IPatientAssignment <T>
    {
        Task<List<T>> GetNewlyRegisteredPatientAsnyc();
        Task<GeneralResponse> AssignPatientToDietAsync(int patientId, int dietitanId);
        Task<GeneralResponse> UnassignPatientAsync(int patientId);
        Task<List<T>> GetPatientDietianPair();
        Task<GeneralResponse> CompleteProfile(int userId, CompleteProfileDTO completeProfileDTO);
    }
}
