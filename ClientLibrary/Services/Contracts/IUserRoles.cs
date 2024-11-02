namespace ClientLibrary.Services.Contracts
{
    public interface IUserRoles<T>
    {
        Task<List<T>> GetRolesAsync();
    }
}
