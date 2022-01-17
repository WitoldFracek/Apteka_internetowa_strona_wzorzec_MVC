using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PO_Projekt.Models
{
    public class User
    {
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Email address cannot exceed {1} characters")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Password cannot exceed {1} characters")]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
