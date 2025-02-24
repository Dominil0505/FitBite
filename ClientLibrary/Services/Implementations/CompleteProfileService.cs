using BaseLibrary.DTOs.Patient;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class CompleteProfileService(GetHttpClient getHttpClient) : ICompleteProfile
    {
        public async Task<GeneralResponse> CompleteProfile(CompleteProfileDTO completeProfile, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/complete-profile", completeProfile);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

            return result!;
        }

        public async Task<GeneralResponse> IsProfileCompleted(int user_id, string baseUrl)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<GeneralResponse>($"{baseUrl}/is-profile-completed/{user_id}");
            return result!;
        }
    }
}
