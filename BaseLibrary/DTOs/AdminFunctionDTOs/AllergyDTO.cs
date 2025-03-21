﻿using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs.AdminFunctionDTOs
{
    public class AllergyDTO
    {
        public int Allergy_Id { get; set; }

        [Required(ErrorMessage = "Allergy name is required")]
        public string? Allergy_Name { get; set; }
    }
}
