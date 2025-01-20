using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs
{
    public class Register : AccountBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Full name is Required")]
        public string? Name { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords is not match")]
        [Required(ErrorMessage = "Confirmation password is required")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select Role!")]
        public string? Role { get; set; }

    }
}
