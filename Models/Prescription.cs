using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace PO_Projekt.Models
{
    public class Prescription
    {
        /// <summary>
        /// A stored in the local database index of a prescription.
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// A code of the prescription from outside database. It is used to verify the prescription with the outside source.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int PrescriptionCode { get; set; }
        /// <summary>
        /// Starting date from which the prescription is valid.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Prescription expiary date.
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Stored in the local database foreign key from the user table.
        /// </summary>
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [NotMapped]
        public List<Prescription> PrescriptionList { get; set; }
        [NotMapped]
        [Required]
        public int Code { get; set; }
        [NotMapped]
        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "Niepoprawna wartość numeru pesel.")]
        public string Pesel { get; set; }


    }
}
