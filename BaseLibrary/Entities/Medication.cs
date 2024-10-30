using BaseLibrary.EntitiesRelation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    [Index(nameof(Medication_Name), IsUnique = true)]

    public class Medication
    {
        [Key]
        public int Medication_Id { get; set; }
        public string? Medication_Name { get; set; }

        // Relation N:M
        public virtual ICollection<Patient_Medication> PatientMedications { get; set; } = new List<Patient_Medication>();
    }
}
