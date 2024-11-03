using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class FoodRepository(AppDbContext _context) : IGenericRepositoryInterface<AddFoodsDTO>
    {
        public async Task<GeneralResponse> DeleteById(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food is null) return NotFound("Food is not found");

            _context.Foods.Remove(food);
            await Commit();
            return Success();
        }

        public async Task<List<AddFoodsDTO>> GetAll()
        {
            return await _context.Foods
                .Include(i => i.FoodIngredients)
                .Include(a => a.FoodAllergies)
                .Select(f => new AddFoodsDTO
                {
                    foods = f,
                    FoodIngredient = f.FoodIngredients.FirstOrDefault(),
                    FoodAllergies = f.FoodAllergies.FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<AddFoodsDTO> GetById(int id)
        {
            return await _context.Foods
                .Where(f => f.Food_Id == id)
                .Include(i => i.FoodIngredients)
                .Include(a => a.FoodAllergies)
                .Select(f => new AddFoodsDTO
                {
                    foods = f,
                    FoodIngredient = f.FoodIngredients.FirstOrDefault(),
                    FoodAllergies = f.FoodAllergies.FirstOrDefault()
                }).FirstOrDefaultAsync();
        }

        public async Task<GeneralResponse> Insert(AddFoodsDTO item)
        {
            if (!await CheckName(item.foods.Food_Name!)) return new GeneralResponse(false, "Food is already added");
            if (item.FoodIngredient.IngredientId != null) return new GeneralResponse(false, "Ingredient is empty! Please chose an ingredient");

            _context.Foods.Add(item.foods);
            _context.Food_Ingredients.Add(item.FoodIngredient);
            _context.Food_Allergies.Add(item.FoodAllergies);

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(AddFoodsDTO item)
        {
            var food = await _context.Foods.FindAsync(item.foods.Food_Id);
            if (food is null) return NotFound("Food is not found");

            var food_allergy = await _context.Food_Allergies.FindAsync(food.Food_Id);
            var food_ingredient = await _context.Food_Ingredients.FindAsync(food.Food_Id);

            if (food_allergy is null) return NotFound("Food allergy is not found");
            if (food_ingredient is null) return NotFound("Food ingredient is not found");


            // Add food data
            food.Food_Name = item.foods.Food_Name;
            food.Calorie = item.foods.Calorie;
            food.Protein = item.foods.Protein;
            food.Fat = item.foods.Fat;
            food.Carbohydrate = item.foods.Carbohydrate;

            // Food allergy relation
            food_allergy.AllergyId = item.FoodAllergies.AllergyId;
            food_allergy.FoodId = item.FoodAllergies.FoodId;

            // food ingredient relation
            food_ingredient.IngredientId = item.FoodIngredient.IngredientId;
            food_ingredient.FoodId = item.FoodIngredient.FoodId;

            await Commit();
            return Success();
        }

        private static GeneralResponse NotFound(string message) => new(false, message);
        private static GeneralResponse Success() => new(true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name)
        {
            var item = await _context.Foods.FirstOrDefaultAsync(_ => _.Food_Name!.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}
