namespace BaseLibrary.DTOs.Profile
{
    public class DietitianProfileDTO : BasicProfile
    {
        public int max_patient_number {  get; set; }
        public int patient_number {  get; set; }
        public IEnumerable<string>? patient_name{ get; set; }
    }
}
