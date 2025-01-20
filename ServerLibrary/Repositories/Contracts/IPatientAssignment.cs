using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IPatientAssignment <T>
    {
        Task<List<T>> GetPatientAsnyc();
        Task<GeneralResponse> AssignPatientToDietAsync(T AssignPatient);
        Task<GeneralResponse> UnassignPatientAsync(int patientId);
    }
}
