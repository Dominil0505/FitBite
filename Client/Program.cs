using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.DTOs.Patient;
using BaseLibrary.DTOs.Profile;
using BaseLibrary.Entities;
using Blazored.LocalStorage;
using Client;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddMudExtensions();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7244");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();


// Roles for registration page
builder.Services.AddScoped<IUserRoles<Roles>, UserRolesService<Roles>>();

// Allergy | Disease | Ingredient | Medication
builder.Services.AddScoped<IGenericServiceInterface<AllergyDTO>, GenericServiceImplementation<AllergyDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<DiseaseDTO>, GenericServiceImplementation<DiseaseDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<IngredientDTO>, GenericServiceImplementation<IngredientDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<MedicationDTO>, GenericServiceImplementation<MedicationDTO>>();
builder.Services.AddScoped<IGenericServiceInterface<FoodsDTO>, GenericServiceImplementation<FoodsDTO>>();

// Admin - Patient Functions
builder.Services.AddScoped<IPatientAssignment<PatientDTO>, PatientAssignment<PatientDTO>>();

// Dietitan
builder.Services.AddScoped<IAvailableDiet, AvailableDietService>();

// Patient - Complete profile
builder.Services.AddScoped<ICompleteProfile, CompleteProfileService>();

// Profile
builder.Services.AddScoped<IProfile<PatientProfileDTO>, ProfileService<PatientProfileDTO>>();
builder.Services.AddScoped<IProfile<DietitianProfileDTO>, ProfileService<DietitianProfileDTO>>();
builder.Services.AddScoped<IProfile<AdminProfileDTO>, ProfileService<AdminProfileDTO>>();
builder.Services.AddScoped<IProfile<CompleteProfileDTO>, ProfileService<CompleteProfileDTO>>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();


await builder.Build().RunAsync();
