using BaseLibrary.EntitiesRelation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    [Index(nameof(Ingredient_Name), IsUnique = true)]
    public class Ingredient
    {
        [Key]
        public int Ingredient_Id { get; set; }
        public string? Ingredient_Name { get; set; }
        public double? Calorie { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }
        public double? Carbohydrate { get; set; }

        // Relation N:M 
        public virtual ICollection<Food_Ingredients> FoodIngredients { get; set; } = new List<Food_Ingredients>();
    }
}

