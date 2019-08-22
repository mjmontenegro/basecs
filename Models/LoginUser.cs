using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace crudi.Models
    {
        public class LoginUser
        {
            [EmailAddress]
            [Required]
            [Display(Name = "Email")]
            public string LoginEmail {get;set;}
            [DataType(DataType.Password)]
            [Required]
            [Display(Name = "Password")]
            // [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            public string LoginPassword {get;set;}
        }
    }
    