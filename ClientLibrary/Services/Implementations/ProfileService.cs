using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class ProfileService<T>(GetHttpClient gethttpClient) : IProfile<T>
    {
        /* GET */
        public async Task<T> getProfile(int user_id, string profileType)
        {
            var httpClient = await gethttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<T>($"get-{profileType}-profile/{user_id}");

            return result!;
        }

        /* PUT */
        public async Task<GeneralResponse> updateProfile(T profile, string profileType)
        {
            var httpClient = await gethttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"update-{profileType}-profile", profile);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

            return result!;
        }

        /* DELETE */
        public async Task<GeneralResponse> deleteProfile(int user_id, string profileType)
        {
            var httpClient = await gethttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"/delete-{profileType}-profile/{user_id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

            return result!;
        }
    }
}
