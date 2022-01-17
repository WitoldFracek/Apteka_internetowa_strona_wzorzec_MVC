using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    public class ProductName
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short name")]
        [MaxLength(20, ErrorMessage = " To long name, do not exceed {1}")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public string RequiresPrescription { get; set; }

        [Required]
        public string Description { get; set; }

        public Manufacturer Manufacturer { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
    }
}
