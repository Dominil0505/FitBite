
namespace ServerLibrary.Repositories.Contracts
{
    public interface IDietitianInterface<T>
    {
        Task<List<T>> GetAvailableDietitans();
    }
}
