using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Linq;
// using crudi.Models


namespace crudi.Models
    {
        public class NoPastDates : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if ((DateTime)value <= DateTime.Now)
                {
                    return new ValidationResult("No Past Dates Allowed!");
                }
                return ValidationResult.Success;
            }
        }
        public class NoUnder13b : ValidationAttribute
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

        public class Wedding
        {
            [Key]
            [Column("id")]
            public int WeddingId {get;set;}

            [Required]
            [Display(Name = "Wedder One")]
            [Column("wedder_one", TypeName="VARCHAR(45)")]
            public string WedderOne {get;set;}

            [Required]
            [Display(Name = "Wedder Two")]
            [Column("wedder_two", TypeName="VARCHAR(45)")]
            public string WedderTwo {get;set;}

            [Required]
            [Display(Name = "Wedding Address")]
            [Column("address", TypeName="VARCHAR(45)")]
            public string Address {get;set;}

            [Required]
            // [NoUnder13]
            [Display(Name = "Date")]
            [DataType(DataType.DateTime)]
            public DateTime Date {get;set;}

            [Required]
            public int UserId {get;set;}
            
            //Navigation Properties
            public List<RSVP> Attendees {get;set;}
        }
    }