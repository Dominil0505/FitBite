
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class IngredientRepository(AppDbContext _context) : IGenericRepositoryInterface<Ingredient>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient is null) return NotFound();

            _context.Ingredients.Remove(ingredient);
            await Commit();
            return Success();
        }

        public async Task<List<Ingredient>> GetAll() => await _context.Ingredients.ToListAsync();

        public async Task<Ingredient> GetById(int id) => await _context.Ingredients.FindAsync(id);

        public async Task<GeneralResponse> Insert(Ingredient item)
        {
            var ingredient = await _context.Ingredients.FindAsync(item.Ingredient_Id);
            if (ingredient is null) return NotFound();

            ingredient.Ingredient_Name = item.Ingredient_Name;
            ingredient.Calorie = item.Calorie;
            ingredient.Protein = item.Protein;
            ingredient.Fat = item.Fat;
            ingredient.Carbohydrate = item.Carbohydrate;

            await Commit();
            return Success();
        }

        public Task<GeneralResponse> Update(Ingredient item)
        {
            throw new NotImplementedException();
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, allergies not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Ingredients.FirstOrDefaultAsync(_ => _.Ingredient_Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}