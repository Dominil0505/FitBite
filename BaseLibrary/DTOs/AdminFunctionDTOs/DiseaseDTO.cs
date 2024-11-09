using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class DiseaseDTO
    {
        public int Disease_Id { get; set; }

        [Required(ErrorMessage = "Disease name is required")]
        public string? Disease_Name { get; set; }
    }
}
