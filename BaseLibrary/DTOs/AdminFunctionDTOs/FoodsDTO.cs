using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class FoodsDTO
    {
        public int Food_Id { get; set; }

        [Required(ErrorMessage = "Food name is required!")]
        public string Food_Name { get; set; }
        public double Calorie { get; set; }
        public double Protein { get; set; } 
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }

        [Required(ErrorMessage = "Ingredients is required")]
        public List<string>? selectedIngredients { get; set; } = new List<string>();
        public List<string>? selectedAllergy { get; set; } = new List<string>();

    }
}
