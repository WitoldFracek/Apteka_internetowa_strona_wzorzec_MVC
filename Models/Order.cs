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
        public ShippingData ShippingData { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public enum ShippingTypes { get; set; }


    }
}
