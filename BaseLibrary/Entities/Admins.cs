using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities
{
    public class Admins
    {
        [Key]
        public int Admin_Id { get; set; }

        // Relations | foreign key to Users table
        [ForeignKey("Users")]
        public int User_Id{ get; set; }
        public virtual Users? Users { get; set; }

    }
}
