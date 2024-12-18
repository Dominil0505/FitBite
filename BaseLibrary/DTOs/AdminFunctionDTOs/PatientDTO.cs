namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class PatientDTO
    {
        public int Patient_Id { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_Email { get; set; }
        public int? Dietitan_Id { get; set; }
        public string Dietitan_Name { get; set; }
        public bool IsProfileCompleted { get; set; }
    }
}
