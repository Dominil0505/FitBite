using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class IngredientDTO
    {
        public int Ingredient_Id { get; set; }

        [Required(ErrorMessage = "Ingredient name is required")]
        public string Ingredient_Name { get; set; }
        public double Calorie { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }
    }
}
