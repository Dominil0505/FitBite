using BaseLibrary.Entities;

namespace BaseLibrary.EntitiesRelation
{
    public class User_Roles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // Relations
        public virtual Users? User { get; set; }
        public virtual Roles? Role { get; set; }
    }
}
