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
        [NotMapped]
        public static string IMAGE = "/image/";
        [NotMapped]
        public static string DefaultImage = "/image/no_image.jpg";

        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "To short name")]
        [MaxLength(50, ErrorMessage = " To long name, do not exceed {1}")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        public bool RequiresPrescription { get; set; }

        [Required]
        public string Description { get; set; }
        [NotMapped]
        public Manufacturer Manufacturer { get; set; }
        [Required]
        public int ManufacturerId { get; set; }

        public string ImageFilename { get; set; }

        [NotMapped]
        public string PhotoRelativePath
        {
            get { return ImageFilename != null && !ImageFilename.Equals("") ? IMAGE + ImageFilename : DefaultImage; }
        }

        public int ProductFormId { get; set; }
        [NotMapped]
        public ProductForm ProductForm { get; set; }

    }
}
