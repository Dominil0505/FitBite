using BaseLibrary.EntitiesRelation;
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IFoodInterface<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<GeneralResponse> Insert(T item, Food_Ingredients food_ingredient, Food_Allergies food_allergy);
        Task<GeneralResponse> Update(T item, Food_Ingredients food_ingredient, Food_Allergies food_allergy);
        Task<GeneralResponse> DeleteById(int id);
    }
}
