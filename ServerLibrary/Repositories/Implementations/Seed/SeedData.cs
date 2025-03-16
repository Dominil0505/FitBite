using BaseLibrary.DTOs.AdminFunctionDTOs;
using BaseLibrary.Entities;

namespace ServerLibrary.Repositories.Implementations.Seed
{
    public static class SeedData
    {
        public static List<Allergies> GetAllergies(){
            return new List<Allergies>{
            new Allergies { Allergy_Name = "Peanut" },
            new Allergies { Allergy_Name = "Tree Nut" },
            new Allergies { Allergy_Name = "Milk" },
            new Allergies { Allergy_Name = "Egg" },
            new Allergies { Allergy_Name = "Wheat" },
            new Allergies { Allergy_Name = "Soy" },
            new Allergies { Allergy_Name = "Fish" },
            new Allergies { Allergy_Name = "Shellfish" },
            new Allergies { Allergy_Name = "Sesame" },
            new Allergies { Allergy_Name = "Mustard" },
            new Allergies { Allergy_Name = "Corn" },
            new Allergies { Allergy_Name = "Lupin" },
            new Allergies { Allergy_Name = "Celery" },
            new Allergies { Allergy_Name = "Kiwi" },
            new Allergies { Allergy_Name = "Banana" },
            new Allergies { Allergy_Name = "Avocado" },
            new Allergies { Allergy_Name = "Tomato" },
            new Allergies { Allergy_Name = "Strawberry" },
            new Allergies { Allergy_Name = "Mango" },
            new Allergies { Allergy_Name = "Peach" },
            new Allergies { Allergy_Name = "Apple" },
            new Allergies { Allergy_Name = "Grape" },
            new Allergies { Allergy_Name = "Pineapple" },
            new Allergies { Allergy_Name = "Coconut" },
            new Allergies { Allergy_Name = "Chocolate" }
        };
        }

        public static List<Medication> GetMedications(){
            return new List<Medication>{
            new Medication { Medication_Name = "Penicillin" },
            new Medication { Medication_Name = "Aspirin" },
            new Medication { Medication_Name = "Ibuprofen" },
            new Medication { Medication_Name = "Paracetamol" },
            new Medication { Medication_Name = "Amoxicillin" },
            new Medication { Medication_Name = "Ciprofloxacin" },
            new Medication { Medication_Name = "Metformin" },
            new Medication { Medication_Name = "Prednisone" },
            new Medication { Medication_Name = "Diphenhydramine" },
            new Medication { Medication_Name = "Loratadine" },
            new Medication { Medication_Name = "Cetirizine" },
            new Medication { Medication_Name = "Ranitidine" },
            new Medication { Medication_Name = "Omeprazole" },
            new Medication { Medication_Name = "Atorvastatin" },
            new Medication { Medication_Name = "Simvastatin" },
            new Medication { Medication_Name = "Losartan" },
            new Medication { Medication_Name = "Hydrochlorothiazide" },
            new Medication { Medication_Name = "Amlodipine" },
            new Medication { Medication_Name = "Gabapentin" },
            new Medication { Medication_Name = "Tramadol" },
            new Medication { Medication_Name = "Azithromycin" },
            new Medication { Medication_Name = "Clarithromycin" },
            new Medication { Medication_Name = "Doxycycline" },
            new Medication { Medication_Name = "Morphine" },
            new Medication { Medication_Name = "Codeine" }
        };
        }

        public static List<Diseases> GetDiseases(){
            return new List<Diseases>{
            new Diseases { Disease_Name = "Diabetes Mellitus (Type 1)" },
            new Diseases { Disease_Name = "Diabetes Mellitus (Type 2)" },
            new Diseases { Disease_Name = "Hypertension" },
            new Diseases { Disease_Name = "Obesity" },
            new Diseases { Disease_Name = "Hyperlipidemia" },
            new Diseases { Disease_Name = "Celiac Disease" },
            new Diseases { Disease_Name = "Lactose Intolerance" },
            new Diseases { Disease_Name = "Irritable Bowel Syndrome (IBS)" },
            new Diseases { Disease_Name = "Inflammatory Bowel Disease (IBD)" },
            new Diseases { Disease_Name = "Crohn’s Disease" },
            new Diseases { Disease_Name = "Ulcerative Colitis" },
            new Diseases { Disease_Name = "Gastroesophageal Reflux Disease (GERD)" },
            new Diseases { Disease_Name = "Chronic Kidney Disease (CKD)" },
            new Diseases { Disease_Name = "Liver Cirrhosis" },
            new Diseases { Disease_Name = "Fatty Liver Disease (NAFLD)" },
            new Diseases { Disease_Name = "Metabolic Syndrome" },
            new Diseases { Disease_Name = "Gout" },
            new Diseases { Disease_Name = "Osteoporosis" },
            new Diseases { Disease_Name = "Anemia" },
            new Diseases { Disease_Name = "Food Allergies" },
            new Diseases { Disease_Name = "Phenylketonuria (PKU)" },
            new Diseases { Disease_Name = "Hypothyroidism" },
            new Diseases { Disease_Name = "Hyperthyroidism" },
            new Diseases { Disease_Name = "Pancreatitis" },
            new Diseases { Disease_Name = "Eating Disorders (Anorexia, Bulimia, Binge Eating)" }
        };

        }

        public static List<Roles> GetRoles(){
            return new List<Roles> { 
                new Roles{ Role_Name = "Admin"},
                new Roles{ Role_Name = "Dietitian"},
                new Roles{ Role_Name = "Patient"}
                };
        }
    }
}