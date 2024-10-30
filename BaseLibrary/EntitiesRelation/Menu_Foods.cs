using BaseLibrary.Entities;

namespace BaseLibrary.EntitiesRelation
{
    public class Menu_Foods
    {
        public int MenuId { get; set; }
        public int FoodId { get; set; }

        // Relations
        public virtual Menus? Menu { get; set; }
        public virtual Foods? Food { get; set; }
    }
}
