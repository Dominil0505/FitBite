using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations.AdminFunctions
{
    public class IngredientRepository(AppDbContext _context) : IGenericRepositoryInterface<IngredientDTO>
    {
        public async Task<List<IngredientDTO>> GetAll()
        {
            var ingredients = await _context.Ingredients.ToListAsync();

            var ingredientsDTOList = ingredients.Select(i => new IngredientDTO
            {
                Ingredient_Id = i.Ingredient_Id,
                Ingredient_Name = i.Ingredient_Name,
                Calorie = i.Calorie,
                Protein = i.Protein,
                Fat = i.Fat,
                Carbohydrate = i.Carbohydrate,
            }).ToList();

            return ingredientsDTOList;
        }

        public async Task<GeneralResponse> Insert(IngredientDTO item)
        {
            if (!await CheckName(item.Ingredient_Name)) return new GeneralResponse(false, "Ingredient is already added");

            var newIngredient = new Ingredient
            {
                Ingredient_Name = item.Ingredient_Name,
                Calorie = item.Calorie,
                Protein = item.Protein,
                Fat = item.Fat,
                Carbohydrate = item.Carbohydrate
            };

            _context.Ingredients.Add(newIngredient);
            await Commit();
            return Success();
        }

        public async Task<GeneralResponse> Update(IngredientDTO item)
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

        public async Task<GeneralResponse> DeleteById(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient is null) return NotFound();

            _context.Ingredients.Remove(ingredient);
            await Commit();
            return Success();
        }

        public async Task<IngredientDTO> GetById(int id) 
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            var ingredientDTO = new IngredientDTO
            {
                Ingredient_Id = ingredient.Ingredient_Id,
                Ingredient_Name = ingredient.Ingredient_Name,
                Calorie = ingredient.Calorie,
                Protein = ingredient.Protein,
                Fat = ingredient.Fat,
                Carbohydrate = ingredient.Carbohydrate
            };

            return ingredientDTO;
        }

        private static GeneralResponse NotFound() => new(false, "Sorry, Ingredient not found");
        private static GeneralResponse Success() => new (true, "Process Completed");
        private async Task Commit() => await _context.SaveChangesAsync();

        private async Task<bool> CheckName(string name){
            var item = await _context.Ingredients.FirstOrDefaultAsync(_ => _.Ingredient_Name.ToLower().Equals(name.ToLower()));
            return item is null;
        }
    }
}