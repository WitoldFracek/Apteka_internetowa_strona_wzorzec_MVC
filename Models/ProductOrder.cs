using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    public class ProductOrder
    {
        [Required]
        public int Id { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        [Required]
        public int ProductId { get; set; }
        [NotMapped]
        public Product Order { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
