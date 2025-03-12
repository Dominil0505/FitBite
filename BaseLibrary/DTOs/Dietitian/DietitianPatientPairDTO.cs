namespace BaseLibrary.DTOs.Dietitian
{
    public class DietitianPatientPairDTO
    {
        public int DietitianId {  get; set; }
        public string PatientName {  get; set; }
        public int PatientId { get; set; }

        IEnumerable<string>? PatientAllergies { get; set; } 
        IEnumerable<string>? PatientDisease { get; set; } 
        IEnumerable<string>? PatientMedication { get; set; } 
    }
}
