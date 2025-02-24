using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateUserAsync(Register user);
        Task<LoginResponse> SignInAsync(CustomLogin user);
        Task<LoginResponse> RefreshTokenAsnyc(RefreshToken token);
    }
}
