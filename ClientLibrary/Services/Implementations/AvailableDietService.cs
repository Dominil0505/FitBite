using BaseLibrary.DTOs.Dietitian;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class AvailableDietService(GetHttpClient gethtppClient) : IAvailableDiet
    {
        public async Task<List<AvailableDietDTO>> GetAvailableDiet(string baseUrl)
        {
            var httpClient = await gethtppClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<AvailableDietDTO>>($"{baseUrl}/available-dietitians");
            return result!;
        }
    }
}
