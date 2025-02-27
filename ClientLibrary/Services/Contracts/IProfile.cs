using BaseLibrary.DTOs.Profile;
using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IProfile<T>
    {
        Task<T> getProfile(int user_id, string profileType);

        Task<GeneralResponse> updateProfile(T profile, string profileType);

        Task<GeneralResponse> deleteProfile(int user_id, string profileType);

    }
}
