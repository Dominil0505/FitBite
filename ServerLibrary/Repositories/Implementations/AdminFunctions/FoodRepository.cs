using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class FoodRepository(AppDbContext _context) : IGenericRepository<FoodsDTO>
    {
        public async Task<List<FoodsDTO>> GetAll()
        {
            var foods = await _context.Foods
                .Include(f => f.FoodIngredients)
                    .ThenInclude(fi => fi.Ingredient)
                .Include(f => f.FoodAllergies)
                    .ThenInclude(fa => fa.Allergy)
                .ToListAsync();

            var foodDTOList = foods.Select(f => new FoodsDTO
            {
                Food_Id = f.Food_Id,
                Food_Name = f.Food_Name,
                Calorie = f.Calorie,
                Protein = f.Protein,
                Fat = f.Fat,
                Carbohydrate = f.Carbohydrate,
                selectedIngredients = f.FoodIngredients
                    .Select(fi => fi.Ingredient.Ingredient_Name)
                    .ToList(),
                selectedAllergy = f.FoodAllergies
                    .Select(fa => fa.Allergy.Allergy_Name)
                    .ToList()
            }).ToList();

            return foodDTOList;
        }

        public async Task<GeneralResponse> Insert(FoodsDTO item)
        {
            if (!await CheckName(item.Food_Name!)) return new GeneralResponse(false, "Food is already added");
            if (item.selectedIngredients == null) return new GeneralResponse(false, "Food is empty! Please choose an food");

            var newFood = new Foods
            {
                Food_Name = item.Food_Name,
                Calorie = item.Calorie,
                Protein = item.Protein,
                Fat = item.Fat,
                Carbohydrate = item.Carbohydrate
            };

            _context.Foods.Add(newFood);
            await Commit();

            if (item.selectedIngredients != null && item.selectedIngredients.Any())
            {
                foreach (var selectedIng in item.selectedIngredients)
                {
                    var selectedIngredient_id = await _context.Ingredients.FirstOrDefaultAsync(_ => _.Ingredient_Name == selectedIng);
                    Food_Ingredients food_Ingredients = new Food_Ingredients
                    {
                        FoodId = newFood.Food_Id,
                        IngredientId = selectedIngredient_id!.Ingredient_Id
                    };

                    _context.Food_Ingredients.Add(food_Ingredients);
                }
            }

            if (item.selectedAllergy != null && item.selectedAllergy.Any())
            {
                foreach (var selectedAll in item.selectedAllergy)
                {
                    var selectedAllergy_id = await _context.Allergy.FirstOrDefaultAsync(_ => _.Allergy_Name == selectedAll);
                    Food_Allergies food_allergy = new Food_Allergies
                    {
                        FoodId = newFood.Food_Id,
                        AllergyId = selectedAllergy_id.Allergy_Id
                    };
                    _context.Food_Allergies.Add(food_allergy);
                }
            }

            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(FoodsDTO item)
        {
            var food = await _context.Foods.FindAsync(item.Food_Id);
            if (food == null) return NotFound("Food is not found");

            food.Food_Name = item.Food_Name;
            food.Calorie = item.Calorie;
            food.Protein = item.Protein;
            food.Fat = item.Fat;
            food.Carbohydrate = item.Carbohydrate;

            if (item.selectedIngredients != null && item.selectedIngredients.Any())
            {
                var existingIngredients = _context.Food_Ingredients.Where(fi => fi.FoodId == food.Food_Id);
                _context.Food_Ingredients.RemoveRange(existingIngredients);

                foreach (var selectedIng in item.selectedIngredients)
                {
                    var selectedIngredient = await _context.Ingredients.FirstOrDefaultAsync(_ => _.Ingredient_Name == selectedIng);
                    if (selectedIngredient != null)
                    {
                        _context.Food_Ingredients.Add(new Food_Ingredients
                        {
                            FoodId = food.Food_Id,
                            IngredientId = selectedIngredient.Ingredient_Id
                        });
                    }
                }
            }


            if (item.selectedAllergy != null && item.selectedAllergy.Any())
            {
                var existingAllergies = _context.Food_Allergies.Where(fa => fa.FoodId == food.Food_Id);
                _context.Food_Allergies.RemoveRange(existingAllergies);

                foreach (var selectedAll in item.selectedAllergy)
                {
                    var selectedAllergyItem = await _context.Allergy.FirstOrDefaultAsync(_ => _.Allergy_Name == selectedAll);
                    if (selectedAllergyItem != null)
                    {
                        _context.Food_Allergies.Add(new Food_Allergies
                        {
                            FoodId = food.Food_Id,
                            AllergyId = selectedAllergyItem.Allergy_Id
                        });
                    }
                }
            }

            await Commit();

            return Success();
        }

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food is null) return NotFound("Food is not found");

            _context.Foods.Remove(food);
            await Commit();
            return Success();
        }

        public async Task<FoodsDTO> GetById(int id)
        {
            return null;
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
