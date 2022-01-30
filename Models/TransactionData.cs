using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Models
{
    public class TransactionData
    {
        [Required]
        public string DeliveryOption { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }

        [Required]
        public int PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        public (string, string, int) GetPersonalData()
        {
            return (Name, LastName, Phone);
        }

        public string GetConcatenatedPersonalData(string separator = " ")
        {
            if (LastName == null)
                throw new ArgumentNullException();
            return $"{Name}{separator}{LastName}{separator}{Phone}";
        }
    }
}
