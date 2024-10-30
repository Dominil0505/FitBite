using BaseLibrary.Entities;

namespace BaseLibrary.EntitiesRelation
{
    public class Patient_Medication
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }

        // Relations
        public virtual Patients? Patient { get; set; }
        public virtual Medication? Medication { get; set; }

    }
}
