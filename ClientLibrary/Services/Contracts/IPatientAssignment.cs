using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IPatientAssignment <T>
    {
        Task<List<T>> GetPatientAsnyc(string baseURL);
        Task<GeneralResponse> AssignAsync(T AssignPatient, string baseURL);
        Task<GeneralResponse> UnassignAsync(T patient, string baseURL);
        Task<T> GetPatientByIdAsync(int patientId, string baseURL);
    }
}
