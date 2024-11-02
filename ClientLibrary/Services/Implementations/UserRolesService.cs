using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class UserRolesService<T>(GetHttpClient getHttpClient) : IUserRoles<T>
    {
        public const string AuthUrl = "api/Roles";
        public async Task<List<T>> GetRolesAsync()
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<T>>($"{AuthUrl}/Roles");

            return result!;
        }
    }
}
