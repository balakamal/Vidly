using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class IsAbove18Years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.None || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            if (customer.DateOfBirth == null)
                return new ValidationResult("The field Date of Birth is required");
            if (DateTime.Today.Year - customer.DateOfBirth.Value.Year >= 18)
                return ValidationResult.Success;
            return new ValidationResult("Customer should be atleast 18 years old to subscribe");
        }
    }
}