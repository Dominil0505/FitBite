using BaseLibrary.EntitiesRelation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    [Index(nameof(User_Name))]
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [Key]
        public int User_Id { get; set; }
        public string? User_Name { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Mobile { get; set; }

        public string? Avatar_Url { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; }
        public DateTime? Deleted_At { get; set; }
        public bool? Is_profile_completed { get; set; } = false;

        // Relations
        public virtual ICollection<User_Roles>? UserRoles { get; set; } = new List<User_Roles>();
        public virtual Dieticians? Dieticians{ get; set; }
        public virtual Admins? Admins{ get; set; }
        public virtual Patients? Patients{ get; set; }

    }
}
