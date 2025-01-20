using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class PatientDTO
    {
        
        public int? Patient_Id { get; set; }
        public string? Patient_Name { get; set; }
        public string? Patient_Email { get; set; }
        public int? Dietitian_Id { get; set; }
        public string? Dietitian_Name { get; set; }
        public bool? IsProfileCompleted { get; set; }
        public string? Status { get; set; }
        public DateTime? AssignDate { get; set; }
    }
}
