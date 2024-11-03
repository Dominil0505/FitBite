using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;

namespace BaseLibrary.DTOs
{
    public class AddFoodsDTO
    {
        public int Food_Id { get; set; }
        public string Food_Name { get; set; }
        public double Calorie { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }

        public List<int> SelectedAllergies { get; set; } = new List<int>();
        public List<int> SelectedIngredients { get; set; } = new List<int>();

        public Foods foods { get; set; }
        public Food_Ingredients FoodIngredient { get; set; }
        public Food_Allergies FoodAllergies { get; set; }
    }
}
