using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Models
{
    public class ShippingData
    {
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "City name cannot be longer than {1} characters")]
        public string City { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Street name cannot be longer than {1} characters")]
        public string Street { get; set; }

        [Required]
        [MaxLength(7, ErrorMessage = "Home number cannot be longer than {1} characters")]
        public string HomeNumber { get; set; }

        [MaxLength(7, ErrorMessage = "Local number cannot be longer than {1} characters")]
        public string LocalNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int PostalNumber { get; set; }
    }
}
