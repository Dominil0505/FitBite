using BaseLibrary.EntitiesRelation;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    public class Roles
    {
        [Key]
        public int Role_Id { get; set; }
        public string? Role_Name { get; set; }
        public virtual ICollection<User_Roles>? Users { get; set; } = new List<User_Roles>();
    }
}
