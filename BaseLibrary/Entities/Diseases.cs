using BaseLibrary.EntitiesRelation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Entities
{
    [Index(nameof(Disease_Name), IsUnique = true)]
    public class Diseases
    {
        [Key]
        public int Disease_Id { get; set; }
        public string? Disease_Name { get; set; }
        public string? Diagnosis_Date { get; set; }

        // Relation N:M
        public virtual ICollection<Patient_Disease> PatientDiseases { get; set; } = new List<Patient_Disease>();
    }
}
