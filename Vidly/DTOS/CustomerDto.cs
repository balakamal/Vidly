using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOS
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsSubscribed { get; set; }

        //[IsAbove18Years]
        public DateTime? DateOfBirth { get; set; }
        public byte MembershipTypeId { get; set; }
    }
}