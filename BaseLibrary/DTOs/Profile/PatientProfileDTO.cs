namespace BaseLibrary.DTOs.Profile
{
    public class PatientProfileDTO : BasicProfile
    {

        public string? dietitan_name{ get; set; }
        public byte? height{ get; set; }
        public short? weight{ get; set; }
        public string? gender { get; set; }
        public IEnumerable<string>? selectedAllergyName { get; set; }
        public IEnumerable<string>? selectedMedicationName { get; set; }
        public IEnumerable<string>? selectedDiseaseName { get; set; }
    }
}
