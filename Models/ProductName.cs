﻿using System;
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
        [MaxLength(100, ErrorMessage = " To long name, do not exceed {1}")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Requires prescription")]
        public bool RequiresPrescription { get; set; }

        [Required]
        public string Description { get; set; }
        public Manufacturer Manufacturer { get; set; }
        [Required]
        public int ManufacturerId { get; set; }

        public string ImageFilename { get; set; }

        [NotMapped]
        public string PhotoRelativePath
        {
            get { return ImageFilename != null && !ImageFilename.Equals("") ? IMAGE + ImageFilename : DefaultImage; }
        }
        [Required]
        public int ProductFormId { get; set; }
        [Display(Name = "Form")]
        public ProductForm ProductForm { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
        [Display(Name = "Type")]
        public ProductType ProductType { get; set; }
    }
}
