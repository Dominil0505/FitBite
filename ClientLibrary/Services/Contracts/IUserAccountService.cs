using BaseLibrary.DTOs;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateUserAsync(Register user);
        Task<LoginResponse> SignInAsync(CustomLogin user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
        Task<WeatherForecast[]> GetWeatherForecasts();
    }
}
