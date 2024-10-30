using BaseLibrary.Entities;

namespace BaseLibrary.EntitiesRelation
{
    public class Patient_Allergies
    {
        public int PatientId { get; set; }
        public int AllergyId { get; set; }

        // Relations
        public virtual Patients? Patient { get; set; }
        public virtual Allergies? Allergy { get; set; }

    }
}
