using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PO_Projekt.Models
{
    public class User
    {
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Email address cannot exceed {1} characters")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Password cannot exceed {1} characters")]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        [NotMapped]
        public List<Prescription> AllPrescriptions { get; set; }
        [NotMapped]
        public List<ShippingData> AllShippingData { get; set; }
        [NotMapped]
        public List<ProductName> SelectedProducts { get; set; }
        [Required]
        [NotMapped]
        public string DeliveryOption { get; set; }
        [Required]
        [NotMapped]
        public string PaymentMethod { get; set; }
        [Required]
        [NotMapped]
        [MinLength(2, ErrorMessage = "Pole \"Imię\" jest wymagane.")]
        public string Name { get; set; }
        [Required]
        [NotMapped]
        [MinLength(2, ErrorMessage = "Pole \"Nazwisko\" jest wymagane.")]
        public string LastName { get; set; }
        [Required]
        [NotMapped]
        public int Phone { get; set; }
        [Required]
        [NotMapped]
        [MinLength(2, ErrorMessage = "Pole \"Ulica\" jest wymagane.")]
        public string StreetName { get; set; }
        [Required]
        [NotMapped]
        [MinLength(2, ErrorMessage = "Pole \"Numer domu\" jest wymagane.")]
        public string HouseNumber { get; set; }
        [NotMapped]
        public string LocalNumber { get; set; }
        [Required]
        [NotMapped]
        [Range(10000, 99999, ErrorMessage = "Pole \"Kod pocztowy\" jest wymagane.")]
        public int PostalCode { get; set; }
        [Required]
        [NotMapped]
        [MinLength(2, ErrorMessage = "Pole \"Miasto\" jest wymagane.")]
        public string City { get; set; }
    }
}
