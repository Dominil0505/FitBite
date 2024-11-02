using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs
{
    public class DiseaseDTO
    {
        public int Disease_Id { get; set; }
        [Required(ErrorMessage = "Allergy name is required")]
        public string? Disease_Name { get; set; }
    }
}
