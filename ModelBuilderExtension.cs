using Microsoft.EntityFrameworkCore;
using PO_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PO_Projekt
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer()
                {
                    Id = 1,
                    Name = "Pfizer"
                },
                new Manufacturer()
                {
                    Id = 2,
                    Name = "Novartis"
                }

                );

        }
    }
}
