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

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType()
                {
                    Id = 1,
                    Name = "sprzęt medyczny"
                },
                new ProductType()
                {
                    Id = 2,
                    Name = "kosmetyki"
                },
                new ProductType()
                {
                    Id = 3,
                    Name = "suplementy"
                },
                new ProductType()
                {
                    Id = 4,
                    Name = "przeciwbólowe"
                },
                new ProductType()
                {
                    Id = 5,
                    Name = "antybiotyki"
                },
                new ProductType()
                {
                    Id = 6,
                    Name = "przeziębienie"
                },
                new ProductType()
                {
                    Id = 7,
                    Name = "dziecko"
                },
                new ProductType()
                {
                    Id = 8,
                    Name = "leki"
                },
                new ProductType()
                {
                    Id = 9,
                    Name = "inne"
                }

                );

            modelBuilder.Entity<ProductForm>().HasData(
                new ProductForm()
                {
                    Id = 1,
                    Name = "Tabletka"
                },
                new ProductForm()
                {
                    Id = 2,
                    Name = "Plaster"
                },
                new ProductForm()
                {
                    Id = 3,
                    Name = "Saszetka"
                },
                new ProductForm()
                {
                    Id = 4,
                    Name = "Zawiesina"
                },
                new ProductForm()
                {
                    Id = 5,
                    Name = "Kapsułka"
                },
                new ProductForm()
                {
                    Id = 6,
                    Name = "Proszek"
                },
                new ProductForm()
                {
                    Id = 7,
                    Name = "Opatrunek"
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
                    ManufacturerId = 1,
                    ImageFilename = "xanax.jpg",
                    ProductFormId = 1,
                    ProductTypeId = 8
                },
                new ProductName()
                {
                    Id = 2,
                    Name = "Apap",
                    Price = 6.99,
                    RequiresPrescription = false,
                    Description = "Lek przeciwbólowy i przeciwgorączkowy, który jako substancję czynną zawiera paracetamol. Lek stosuje się w bólach różnego pochodzenia, zarówno głowy, zębów, mięśni jak i menstruacyjnych, kostno-stawowych czy nerwobólach.",
                    ManufacturerId = 3,
                    ImageFilename = "apap.jpg",
                    ProductFormId = 1,
                    ProductTypeId = 6
                },
                new ProductName()
                {
                    Id = 3,
                    Name = "Opaska elastyczna z zapinką",
                    Price = 3.99,
                    RequiresPrescription = false,
                    Description = "To wyrób medyczny, wielokrotnego użytku. Produkt może być stosowany jako opaska podtrzymująca opatrunki, uciskowa oraz usztywniająca okolice okołostawowe. Długość opaski po relaksacji wynosi nie mniej niż 1,5 m.",
                    ManufacturerId = 4,
                    ImageFilename = "opaska-elastyczna-tkana-z-zapinka.jpg",
                    ProductFormId = 7,
                    ProductTypeId = 1
                },
                new ProductName()
                {
                    Id = 4,
                    Name = "Ibuprom Max, 400 mg, tabletki drażowane, 48 szt. (butelka)",
                    Price = 26.49,
                    RequiresPrescription = false,
                    Description = "Ibuprom Max to lek przeciwbólowy, ale także stosuje się go w leczeniu stanu zapalnego. Lek również obniża gorączkę.",
                    ManufacturerId = 1,
                    ImageFilename = "",
                    ProductFormId = 1,
                    ProductTypeId = 8
                },
                new ProductName()
                {
                    Id = 5,
                    Name = "Ibuprom, 200 mg, tabletki powlekane, 10 szt.",
                    Price = 6.99,
                    RequiresPrescription = false,
                    Description = "Produkt leczniczy Ibuprom działa przeciwbólowo, przeciwzapalnie i przeciwgorączkowo. Stosuje się go w bólach głowy, zębów, mięśniowych, okolicy lędźwiowo-krzyżowej, kostnych i stawowych oraz w bolesnym miesiączkowaniu oraz w gorączce.",
                    ManufacturerId = 2,
                    ImageFilename = "",
                    ProductFormId = 1,
                    ProductTypeId = 8
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

            modelBuilder.Entity<ProductOrder>().HasData(
                new ProductOrder()
                {
                    Id = 1,
                    ProductId = 11,
                    OrderId = 4
                },
                new ProductOrder()
                {
                    Id = 2,
                    ProductId = 1,
                    OrderId = 3
                }
                );

        }
    }
}
