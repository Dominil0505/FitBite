
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IPatientAssignment <T>
    {
        Task<List<T>> GetNewlyRegisteredPatientAsnyc(string baseURL);
        Task<GeneralResponse> AssignPatientToDietAsync(int patientId, int dietitanId, string baseURL);
        Task<GeneralResponse> UnassignPatientAsync(int patientId, string baseURL);
        Task<List<T>> GetPatientDietianPair(string baseURL);
    }
}
