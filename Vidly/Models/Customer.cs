using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name ="Is Subscribed")]
        public bool IsSubscribed { get; set; }

        [Display(Name = "Date of Birth (01 Jan 1990)")]
        [IsAbove18Years]
        public DateTime? DateOfBirth { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}