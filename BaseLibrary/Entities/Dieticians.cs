using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BaseLibrary.Entities
{
    public class Dieticians
    {
        [Key]
        public int Dietician_Id { get; set; }
        public string? Description { get; set; }
        public int Patient_Number { get; set; } = 0;
        public int Maximum_Patient_Number { get; set; } = 0;
        

        // Relations | foreign key to Users table
        [ForeignKey("Users")]
        public int? User_Id { get; set; }
        public virtual Users? Users { get; set; }
    }
}
