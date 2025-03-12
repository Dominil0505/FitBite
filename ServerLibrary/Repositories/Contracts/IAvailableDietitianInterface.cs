using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IAvailableDietitianInterface<T>
    {
        Task<List<T>> GetAvailableDietitans();
    }
}
