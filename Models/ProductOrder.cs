using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    /// <summary>
    /// Class that represents the database connection between products and orders.
    /// </summary>
    public class ProductOrder
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        [Required]
        public int Id { get; set; }
        [NotMapped]
        public Product Product { get; set; }
        /// <summary>
        /// Stored in the local database foreign key from the products table.
        /// </summary>
        [Required]
        public int ProductId { get; set; }
        [NotMapped]
        public Order Order { get; set; }
        /// <summary>
        /// Stored in the local database foreign key from the orders table.
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Count of the specified by ProductId product.
        /// </summary>
        [Required]
        public int Count { get; set; }
    }
}
