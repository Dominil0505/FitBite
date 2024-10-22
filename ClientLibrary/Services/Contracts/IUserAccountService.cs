using BaseLibrary.DTOs;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<GeneralResponse> CreateUserAsync(Register user);
        Task<LoginResponse> SignInAsync(CustomLogin user);
        Task<LoginResponse> RefreshTokenAsnyc(RefreshToken token);
        Task<WeatherForecast[]> GetWeatherForecasts();
    }
}
