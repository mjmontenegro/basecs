using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// using System.Collections.Generic;
// using System.Linq;
// using crudi.Models


namespace crudi.Models
    {
        // public class NoFutureDates : ValidationAttribute
        // {
        //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //     {
        //         if ((DateTime)value > DateTime.Now)
        //         {
        //             return new ValidationResult("No Future Dates Allowed!");
        //         }
        //         return ValidationResult.Success;
        //     }
        // }
        // public class NoUnder13 : ValidationAttribute
        // {
        //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //     {
        //         if (((DateTime)value).AddYears(13) > DateTime.Now)
        //         {
        //             return new ValidationResult("No One Under 13 Allowed!");
        //         }
        //         return ValidationResult.Success;
        //     }
        // }

        public class RSVP
        {
            [Key]
            public int RSVPId {get;set;}
            public int UserId {get;set;}
            public int WeddingId {get;set;}
            public User User {get;set;}
            public Wedding Wedding {get;set;}
        }
    }