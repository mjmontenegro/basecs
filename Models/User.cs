using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Collections.Generic;
// using System.Linq;
// using crudi.Models


namespace crudi.Models
    {
        public class NoFutureDates : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if ((DateTime)value > DateTime.Now)
                {
                    return new ValidationResult("No Future Dates Allowed!");
                }
                return ValidationResult.Success;
            }
        }
        public class NoUnder13 : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (((DateTime)value).AddYears(13) > DateTime.Now)
                {
                    return new ValidationResult("No One Under 13 Allowed!");
                }
                return ValidationResult.Success;
            }
        }

        public class User
        {
            [Key]
            // [Column("id")]
            public int UserId {get;set;}

            [Required]
            [Display(Name = "Your First Name:")]
            // [Column("first_name", TypeName="VARCHAR(45)")]
            public string FirstName {get;set;}

            [Required]
            [Display(Name = "Your Last Name:")]
            // [Column("last_name", TypeName="VARCHAR(45)")]
            public string LastName {get;set;}

            [EmailAddress]
            [Required]
            // [Column("email", TypeName="VARCHAR(45)")]
            public string Email {get;set;}

            [DataType(DataType.Password)]
            [Required]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage="Password be at least 8 characters with at least 1 letter, number and special character")]
            // [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            // [Column("password", TypeName="VARCHAR(255)")]
            public string Password {get;set;}

            // Will not be mapped to your users table!
            [NotMapped]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string Confirm {get;set;}

            [Required]
            [NoUnder13]
            [DataType(DataType.DateTime)]  // TEST
            public DateTime Birthdate {get;set;}

            // [Column("created_at")]
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            // [Column("updated_at")]
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            
            //Navigation Properties

            // Full Name Getter
            public string FullName
            {
                get
                {
                    return $"{this.FirstName} {this.LastName}";
                }
            }
        }
    }