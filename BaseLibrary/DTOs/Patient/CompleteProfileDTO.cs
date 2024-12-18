
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Patient
{
    public class CompleteProfileDTO
    {
        [Required(ErrorMessage = "Date of Birth is required!")]
        public DateTime? DoB { get; set; }

        [Required(ErrorMessage = "Height is required")]
        [Range(0, 240, ErrorMessage = "Height must be between 0 and 240 cm")]
        public byte? Height { get; set; }

        [Range(0, 240, ErrorMessage = "Height must be between 0 and 400 kg")]
        [Required(ErrorMessage = "Weight is required")]
        public short? Weight { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
    }
}
