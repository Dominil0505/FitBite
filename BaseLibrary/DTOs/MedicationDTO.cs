using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class MedicationDTO
    {
        public int Medication_Id { get; set; }

        [Required(ErrorMessage = "Medication name is required")]
        public string? Medication_Name { get; set; }
    }
}
