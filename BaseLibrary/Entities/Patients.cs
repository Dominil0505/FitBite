using BaseLibrary.Entities;
using BaseLibrary.EntitiesRelation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities
{
    public class Patients
    {
        [Key]
        public int Patient_Id { get; set; }
        public DateTime? DoB { get; set; }
        public string? Description { get; set; }
        public byte? Height { get; set; }
        public short? Weight { get; set; }
        public string? Gender { get; set; }
        public string? DieticianName { get; set; }
        public int? Created_By_Admin_Id { get; set; }


        // Relation
        [ForeignKey("User")]
        public int? User_Id { get; set; }
        public virtual Users? User { get; set; }

        [ForeignKey("Dietician")]
        public int? Dietician_Id { get; set; }
        public virtual Dieticians? Dietician { get; set; }

        // Relation N:M 
        public virtual ICollection<Patient_Allergies>? PatientAllergies { get; set; } = new List<Patient_Allergies>();
        public virtual ICollection<Patient_Medication>? PatientMedications { get; set; } = new List<Patient_Medication>();
        public virtual ICollection<Patient_Disease>? PatientDiseases { get; set; } = new List<Patient_Disease>();

    }
}


