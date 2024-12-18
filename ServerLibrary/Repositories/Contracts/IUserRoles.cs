namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserRoles<T>
    {
        Task<List<T>> GetAllRoles();
    }
}
