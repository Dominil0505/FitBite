
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.Dietitian
{
    public class MenuToPatientDTO
    {
        [Required(ErrorMessage = "Menu name is required")]
        public string? Menu_Name { get; set; }
        public string? Notes { get; set; }
        public string? Created_by { get; set; }

        [Required(ErrorMessage = "Menu start is required")]
        public DateTime? Menu_Start { get; set; }

        [Required(ErrorMessage = "Menu end is required")]
        public DateTime? Menu_End { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; }
    }
}
