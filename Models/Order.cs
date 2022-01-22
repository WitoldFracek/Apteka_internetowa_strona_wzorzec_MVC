using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    public class Order
    {
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        
        [Required]
        public int ShippingDataId { get; set; }
        
        [ForeignKey("ShippingDataId")]
        [NotMapped]
        public ShippingData ShippingData { get; set; }
        
        
        public int UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Imie jest wymagane")]
        [MaxLength(30, ErrorMessage = "Imie nie może być dzłuższe niż {1} znaków")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(40, ErrorMessage = "Nazwisko nie może być dzłuższe niż {1} znaków")]
        public string LastName { get; set; }

        public int Phone { get; set; }

        [ForeignKey("UserId")]
        [NotMapped]
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public ShippingType ShippingType { get; set; }


    }
}
