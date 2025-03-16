using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ISeeder
    {
        Task<GeneralResponse> SeedAsync();
    }
}