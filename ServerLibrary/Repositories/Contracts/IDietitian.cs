using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IDietitian
    {
        Task<GeneralResponse> AssignMenuToPatient(MenuToPatientDTO menuPatientDTO);
        Task<List<DietitianPatientPairDTO>> GetPatientDietitianPair(int dietitan_id);
    }
}
