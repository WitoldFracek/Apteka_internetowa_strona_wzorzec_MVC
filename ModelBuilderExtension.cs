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
                },
                new Manufacturer()
                {
                    Id = 3,
                    Name = "Us Pharmacia"
                },
                new Manufacturer()
                {
                    Id = 4,
                    Name = "Paso"
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
                    Id = 1,
                    UserId = 1,
                    City = "Wrocław",
                    Street = "Fiołkowa",
                    HomeNumber = "23",
                    LocalNumber = "1",
                    PostalNumber = 50243
                },
                new ShippingData()
                {
                    Id = 2,
                    UserId = 1,
                    City = "Wrocław",
                    Street = "Nizinna",
                    HomeNumber = "2",
                    PostalNumber = 50243
                },
                new ShippingData()
                {
                    Id = 3,
                    UserId = 2,
                    City = "Kraków",
                    Street = "Łańcuchowa",
                    HomeNumber = "29",
                    LocalNumber = "4",
                    PostalNumber = 51753
                },
                new ShippingData()
                {
                    Id = 4,
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
                    Id = 1,
                    PrescriptionCode = 13423,
                    StartDate = new DateTime(2022, 1, 1),
                    EndDate = new DateTime(2022, 2, 1),
                    UserId = 1
                },
                new Prescription()
                {
                    Id = 2,
                    PrescriptionCode = 13778,
                    StartDate = new DateTime(2022, 2, 1),
                    EndDate = new DateTime(2022, 2, 13),
                    UserId = 1
                },
                new Prescription()
                {
                    Id = 3,
                    PrescriptionCode = 14523,
                    StartDate = new DateTime(2022, 1, 12),
                    EndDate = new DateTime(2022, 1, 30),
                    UserId = 3
                }
                );

            modelBuilder.Entity<PrescriptionOrder>().HasData(
                new PrescriptionOrder()
                {
                    Id = 1,
                    OrderId = 1,
                    PrescriptionId = 1
                }
                );

            modelBuilder.Entity<ProductName>().HasData(
                new ProductName()
                {
                    Id = 1,
                    Name = "Xanax",
                    Price = 45.99,
                    RequiresPrescription = true,
                    Description = "Neurologia Psychiatria: nasenne przeciwlękowe przeciwdrgawkowe uspokajające zmniejsza napięcie mięśni",
                    ManufacturerId = 1
                },
                new ProductName()
                {
                    Id = 2,
                    Name = "Apap",
                    Price = 6.99,
                    RequiresPrescription = false,
                    Description = "Lek przeciwbólowy i przeciwgorączkowy, który jako substancję czynną zawiera paracetamol. Lek stosuje się w bólach różnego pochodzenia, zarówno głowy, zębów, mięśni jak i menstruacyjnych, kostno-stawowych czy nerwobólach.",
                    ManufacturerId = 3
                },
                new ProductName()
                {
                    Id = 3,
                    Name = "Opaska elastyczna z zapinką",
                    Price = 3.99,
                    RequiresPrescription = false,
                    Description = "To wyrób medyczny, wielokrotnego użytku. Produkt może być stosowany jako opaska podtrzymująca opatrunki, uciskowa oraz usztywniająca okolice okołostawowe. Długość opaski po relaksacji wynosi nie mniej niż 1,5 m.",
                    ManufacturerId = 4
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    ProductNameId = 1,
                    ExpirationDate = new DateTime(2022, 5, 20)
                },
                new Product()
                {
                    Id = 2,
                    ProductNameId = 1,
                    ExpirationDate = new DateTime(2022, 5, 21)
                },
                new Product()
                {
                    Id = 3,
                    ProductNameId = 1,
                    ExpirationDate = new DateTime(2022, 4, 30)
                },
                new Product()
                {
                    Id = 4,
                    ProductNameId = 2,
                    ExpirationDate = new DateTime(2022, 7, 1)
                },
                new Product()
                {
                    Id = 5,
                    ProductNameId = 2,
                    ExpirationDate = new DateTime(2022, 7, 3)
                },
                new Product()
                {
                    Id = 6,
                    ProductNameId = 2,
                    ExpirationDate = new DateTime(2022, 6, 20)
                },
                new Product()
                {
                    Id = 7,
                    ProductNameId = 2,
                    ExpirationDate = new DateTime(2022, 6, 18)
                },
                new Product()
                {
                    Id = 8,
                    ProductNameId = 3
                },
                new Product()
                {
                    Id = 9,
                    ProductNameId = 3
                },
                new Product()
                {
                    Id = 10,
                    ProductNameId = 3
                },
                new Product()
                {
                    Id = 11,
                    ProductNameId = 3
                }
                );

            //modelBuilder.Entity<ProductOrder>().HasData(
            //    );

        }
    }
}
