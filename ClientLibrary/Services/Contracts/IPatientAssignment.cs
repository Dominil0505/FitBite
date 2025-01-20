using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IPatientAssignment <T>
    {
        Task<List<T>> GetPatientAsnyc(string baseURL);
        Task<GeneralResponse> AssignAsync(T AssignPatient, string baseURL);
        Task<GeneralResponse> UnassignAsync(int patientId, string baseURL);
    }
}
