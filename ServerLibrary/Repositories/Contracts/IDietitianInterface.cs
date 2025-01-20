using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IDietitianInterface<T> : IGenericRepositoryInterface<T>
    {
        Task<List<T>> GetAvailableDietitans();
        Task<GeneralResponse> AssignMenuToPatient(int patient_id, int dietitian_id, MenuToPatientDTO menuPatientDTO);
    }
}
