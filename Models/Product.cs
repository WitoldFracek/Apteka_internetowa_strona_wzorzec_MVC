using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        
        public ProductName ProductName { get; set; }
        [Required]
        public int ProductNameId { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
