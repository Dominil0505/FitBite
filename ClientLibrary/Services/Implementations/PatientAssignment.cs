﻿
using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class PatientAssignment<T>(GetHttpClient getHttpClient) : IPatientAssignment<T>
    {
        public async Task<List<T>> GetPatientAsnyc(string baseURL)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<List<T>>($"{baseURL}/patients");

            return result!;
        }

        public async Task<GeneralResponse> AssignAsync(T AssignPatient, string baseURL)
        {

            var httpClient = await getHttpClient.GetPrivateHttpClient();

            var response = await httpClient.PostAsJsonAsync($"{baseURL}/assign", AssignPatient);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

            return result!;
        }

        public async Task<GeneralResponse> UnassignAsync(int patientId, string baseURL)
        {
            var httpClient = await getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseURL}/unassign", patientId);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

            return result!;
        }
    }
}
