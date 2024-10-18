using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DieticianApp.Models.Entities
{
    public class Dieticians
    {
        [Key]
        public int Dietician_Id { get; set; }
        public string? Description { get; set; }

        // Relations | foreign key to Users table
        [ForeignKey("Users")]
        public int? User_Id { get; set; }
        public virtual Users? Users { get; set; }
    }
}
