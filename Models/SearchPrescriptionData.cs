using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt.Models
{
    public class SearchPrescriptionData
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"\d{11}", ErrorMessage = "Niepoprawna wartość numeru pesel.")]
        public string Pesel { get; set; }

        public void Deconstruct(out int id, out string pesel)
        {
            id = Id;
            pesel = Pesel;
        }
    }
}
