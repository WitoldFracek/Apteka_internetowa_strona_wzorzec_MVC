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

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Email = "ania.haha@gmail.com",
                    Password = "tajnehaslo",
                    BirthDate = new DateTime(2000, 5, 1)
                },
                new User()
                {
                    Id = 2,
                    Email = "korneliusz.smigly@op.pl",
                    Password = "supertajne",
                    BirthDate = new DateTime(1990, 11, 29)
                },
                new User()
                {
                    Id = 3,
                    Email = "sowa.minerwa@o2.pl",
                    Password = "illuminati",
                    BirthDate = new DateTime(1986, 8, 12)
                }
                ) ;

            modelBuilder.Entity<ShippingData>().HasData(
                new ShippingData()
                {
                    Id = 0,
                    UserId = 1,
                    City = "Wrocław",
                    Street = "Fiołkowa",
                    HomeNumber = "23",
                    LocalNumber = "1",
                    PostalNumber = 50243
                },
                new ShippingData()
                {
                    Id = 1,
                    UserId = 1,
                    City = "Wrocław",
                    Street = "Nizinna",
                    HomeNumber = "2",
                    PostalNumber = 50243
                },
                new ShippingData()
                {
                    Id = 2,
                    UserId = 2,
                    City = "Kraków",
                    Street = "Łańcuchowa",
                    HomeNumber = "29",
                    LocalNumber = "4",
                    PostalNumber = 51753
                },
                new ShippingData()
                {
                    Id = 3,
                    UserId = 1,
                    City = "Warszawa",
                    Street = "Długa",
                    HomeNumber = "145",
                    LocalNumber = "10",
                    PostalNumber = 60321
                }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    ShippingDataId = 0,
                    UserId = 1,
                    OrderDate = new DateTime(2022, 1, 1),
                    ShippingType = ShippingType.Delivery
                },
                new Order()
                {
                    Id = 2,
                    ShippingDataId = 1,
                    UserId = 1,
                    OrderDate = new DateTime(2021, 12, 24),
                    ShippingType = ShippingType.ParcelLocker
                },
                new Order()
                {
                    Id = 3,
                    ShippingDataId = 2,
                    UserId = 2,
                    OrderDate = new DateTime(2021, 12, 10),
                    ShippingType = ShippingType.PostOffice
                },
                new Order()
                {
                    Id = 4,
                    ShippingDataId = 2,
                    UserId = 2,
                    OrderDate = new DateTime(2022, 1, 11),
                    ShippingType = ShippingType.Delivery
                }
                );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription()
                {

                }
                );

        }
    }
}
