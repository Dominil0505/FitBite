﻿using BaseLibrary.Entities;

namespace BaseLibrary.EntitiesRelation
{
    public class Patient_Disease
    {
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }

        // Relations
        public virtual Patients? Patient { get; set; }
        public virtual Diseases? Disease { get; set; }

    }
}
