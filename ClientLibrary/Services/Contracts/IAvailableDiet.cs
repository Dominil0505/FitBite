
using BaseLibrary.DTOs.Dietitian;

namespace ClientLibrary.Services.Contracts
{
    public interface IAvailableDiet
    {
        Task<List<AvailableDietDTO>> GetAvailableDiet(string baseUrl);
    }
}
