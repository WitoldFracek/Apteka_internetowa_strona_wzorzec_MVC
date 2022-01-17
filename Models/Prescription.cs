using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Models
{
    public class Prescription
    {
        [Required]
        [Key]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PrescriptionCode { get; set; }
        public DateTime StartDate { get; set }
        public DateTime EndDate { get; set }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }


    }
}
