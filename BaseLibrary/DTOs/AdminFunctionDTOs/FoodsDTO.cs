using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using System.Text.Json.Serialization;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class FoodsDTO
    {
        public int Food_Id { get; set; }
        public string Food_Name { get; set; }
        public double Calorie { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }

        public List<string>? selectedIngredients { get; set; } = new List<string>();
        public List<string>? selectedAllergy { get; set; } = new List<string>();

    }
}
