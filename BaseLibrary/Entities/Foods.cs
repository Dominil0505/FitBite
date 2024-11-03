using BaseLibrary.EntitiesRelation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities
{
    [Index(nameof(Food_Name), IsUnique = true)]
    public class Foods
    {
        [Key]
        public int Food_Id { get; set; }
        public string Food_Name { get; set; }
        public double Calorie { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }

        // Relations N:M
        public virtual ICollection<Food_Ingredients> FoodIngredients { get; set; } = new List<Food_Ingredients>();
        public virtual ICollection<Food_Allergies> FoodAllergies { get; set; } = new List<Food_Allergies>();
        public virtual ICollection<Menu_Foods> MenuFoods { get; set; } = new List<Menu_Foods>();
    }
}
