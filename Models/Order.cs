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
        /// <summary>
        /// A stored in the local database index of an order.
        /// </summary>
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        
        /// <summary>
        /// Stored in the local database foreign key from the shipping data table.
        /// </summary>
        [Required]
        public int ShippingDataId { get; set; }
        
        [ForeignKey("ShippingDataId")]
        [NotMapped]
        public ShippingData ShippingData { get; set; }

        /// <summary>
        /// Stored in the local database foreign key from the user table.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Name of the user to whom the order relates.
        /// </summary>
        [Required]
        [MinLength(2, ErrorMessage = "Imie jest wymagane")]
        [MaxLength(30, ErrorMessage = "Imie nie może być dzłuższe niż {1} znaków")]
        public string Name { get; set; }

        /// <summary>
        /// Last name of the user to whom the order relates.
        /// </summary>
        [Required]
        [MinLength(2, ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(40, ErrorMessage = "Nazwisko nie może być dzłuższe niż {1} znaków")]
        public string LastName { get; set; }

        /// <summary>
        /// Phone number of the user to whom the order relates.
        /// </summary>
        public int Phone { get; set; }

        [ForeignKey("UserId")]
        [NotMapped]
        public User User { get; set; }

        /// <summary>
        /// Order date.
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Shipping type.
        /// </summary>
        public ShippingType ShippingType { get; set; }


    }
}
