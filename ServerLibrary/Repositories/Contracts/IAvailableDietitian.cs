using BaseLibrary.DTOs.Dietitian;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IAvailableDietitian<T>
    {
        Task<List<T>> GetAvailableDietitans();
    }
}
