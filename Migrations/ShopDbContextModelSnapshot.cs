﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PO_Projekt.Data;

namespace PO_Projekt.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    partial class ShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PO_Projekt.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pfizer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Novartis"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Us Pharmacia"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Paso"
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<int>("ShippingDataId")
                        .HasColumnType("int");

                    b.Property<int>("ShippingType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LastName = "Hahałowksa",
                            Name = "Anna",
                            OrderDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Phone = 274654423,
                            ShippingDataId = 0,
                            ShippingType = 3,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            LastName = "Hahałowksa",
                            Name = "Anna",
                            OrderDate = new DateTime(2021, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Phone = 274654423,
                            ShippingDataId = 1,
                            ShippingType = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            LastName = "Śmigły",
                            Name = "Korneliusz",
                            OrderDate = new DateTime(2021, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Phone = 502578335,
                            ShippingDataId = 2,
                            ShippingType = 2,
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            LastName = "Śmigły",
                            Name = "Korneliusz",
                            OrderDate = new DateTime(2022, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Phone = 502578335,
                            ShippingDataId = 2,
                            ShippingType = 3,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PrescriptionCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Prescriptions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndDate = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PrescriptionCode = 13423,
                            StartDate = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            EndDate = new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PrescriptionCode = 13778,
                            StartDate = new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            EndDate = new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PrescriptionCode = 14523,
                            StartDate = new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 3
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.PrescriptionOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PrescriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("PrescriptionOrders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            PrescriptionId = 1
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductNameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductNameId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExpirationDate = new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 1
                        },
                        new
                        {
                            Id = 2,
                            ExpirationDate = new DateTime(2022, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 1
                        },
                        new
                        {
                            Id = 3,
                            ExpirationDate = new DateTime(2022, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 1
                        },
                        new
                        {
                            Id = 4,
                            ExpirationDate = new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 2
                        },
                        new
                        {
                            Id = 5,
                            ExpirationDate = new DateTime(2022, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 2
                        },
                        new
                        {
                            Id = 6,
                            ExpirationDate = new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 2
                        },
                        new
                        {
                            Id = 7,
                            ExpirationDate = new DateTime(2022, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 2
                        },
                        new
                        {
                            Id = 8,
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 3
                        },
                        new
                        {
                            Id = 9,
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 3
                        },
                        new
                        {
                            Id = 10,
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 3
                        },
                        new
                        {
                            Id = 11,
                            ExpirationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ProductNameId = 3
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.ProductForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ProductForms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Tabletka"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Plaster"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Saszetka"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Zawiesina"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Kapsułka"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Proszek"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Opatrunek"
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.ProductName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageFilename")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductFormId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("RequiresPrescription")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ProductFormId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("ProductNames");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Neurologia Psychiatria: nasenne przeciwlękowe przeciwdrgawkowe uspokajające zmniejsza napięcie mięśni",
                            ImageFilename = "xanax.jpg",
                            ManufacturerId = 1,
                            Name = "Xanax",
                            Price = 45.990000000000002,
                            ProductFormId = 1,
                            ProductTypeId = 8,
                            RequiresPrescription = true
                        },
                        new
                        {
                            Id = 2,
                            Description = "Lek przeciwbólowy i przeciwgorączkowy, który jako substancję czynną zawiera paracetamol. Lek stosuje się w bólach różnego pochodzenia, zarówno głowy, zębów, mięśni jak i menstruacyjnych, kostno-stawowych czy nerwobólach.",
                            ImageFilename = "apap.jpg",
                            ManufacturerId = 3,
                            Name = "Apap",
                            Price = 6.9900000000000002,
                            ProductFormId = 1,
                            ProductTypeId = 6,
                            RequiresPrescription = false
                        },
                        new
                        {
                            Id = 3,
                            Description = "To wyrób medyczny, wielokrotnego użytku. Produkt może być stosowany jako opaska podtrzymująca opatrunki, uciskowa oraz usztywniająca okolice okołostawowe. Długość opaski po relaksacji wynosi nie mniej niż 1,5 m.",
                            ImageFilename = "opaska-elastyczna-tkana-z-zapinka.jpg",
                            ManufacturerId = 4,
                            Name = "Opaska elastyczna z zapinką",
                            Price = 3.9900000000000002,
                            ProductFormId = 7,
                            ProductTypeId = 1,
                            RequiresPrescription = false
                        },
                        new
                        {
                            Id = 4,
                            Description = "Ibuprom Max to lek przeciwbólowy, ale także stosuje się go w leczeniu stanu zapalnego. Lek również obniża gorączkę.",
                            ImageFilename = "",
                            ManufacturerId = 1,
                            Name = "Ibuprom Max, 400 mg, tabletki drażowane, 48 szt. (butelka)",
                            Price = 26.489999999999998,
                            ProductFormId = 1,
                            ProductTypeId = 8,
                            RequiresPrescription = false
                        },
                        new
                        {
                            Id = 5,
                            Description = "Produkt leczniczy Ibuprom działa przeciwbólowo, przeciwzapalnie i przeciwgorączkowo. Stosuje się go w bólach głowy, zębów, mięśniowych, okolicy lędźwiowo-krzyżowej, kostnych i stawowych oraz w bolesnym miesiączkowaniu oraz w gorączce.",
                            ImageFilename = "",
                            ManufacturerId = 2,
                            Name = "Ibuprom, 200 mg, tabletki powlekane, 10 szt.",
                            Price = 6.9900000000000002,
                            ProductFormId = 1,
                            ProductTypeId = 8,
                            RequiresPrescription = false
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductOrders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 1,
                            OrderId = 4,
                            ProductId = 11
                        },
                        new
                        {
                            Id = 2,
                            Count = 2,
                            OrderId = 3,
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "sprzęt medyczny"
                        },
                        new
                        {
                            Id = 2,
                            Name = "kosmetyki"
                        },
                        new
                        {
                            Id = 3,
                            Name = "suplementy"
                        },
                        new
                        {
                            Id = 4,
                            Name = "przeciwbólowe"
                        },
                        new
                        {
                            Id = 5,
                            Name = "antybiotyki"
                        },
                        new
                        {
                            Id = 6,
                            Name = "przeziębienie"
                        },
                        new
                        {
                            Id = 7,
                            Name = "dziecko"
                        },
                        new
                        {
                            Id = 8,
                            Name = "leki"
                        },
                        new
                        {
                            Id = 9,
                            Name = "inne"
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.ShippingData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("HomeNumber")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("LocalNumber")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<int>("PostalNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShippingData");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Wrocław",
                            HomeNumber = "23",
                            LocalNumber = "1",
                            PostalNumber = 50243,
                            Street = "Fiołkowa",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            City = "Wrocław",
                            HomeNumber = "2",
                            PostalNumber = 50243,
                            Street = "Nizinna",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            City = "Kraków",
                            HomeNumber = "29",
                            LocalNumber = "4",
                            PostalNumber = 51753,
                            Street = "Łańcuchowa",
                            UserId = 2
                        },
                        new
                        {
                            Id = 4,
                            City = "Warszawa",
                            HomeNumber = "145",
                            LocalNumber = "10",
                            PostalNumber = 60321,
                            Street = "Długa",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ania.haha@gmail.com",
                            Password = "tajnehaslo"
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1990, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "korneliusz.smigly@op.pl",
                            Password = "supertajne"
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(1986, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sowa.minerwa@o2.pl",
                            Password = "illuminati"
                        });
                });

            modelBuilder.Entity("PO_Projekt.Models.Prescription", b =>
                {
                    b.HasOne("PO_Projekt.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PO_Projekt.Models.PrescriptionOrder", b =>
                {
                    b.HasOne("PO_Projekt.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PO_Projekt.Models.Prescription", "Prescription")
                        .WithMany()
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Prescription");
                });

            modelBuilder.Entity("PO_Projekt.Models.Product", b =>
                {
                    b.HasOne("PO_Projekt.Models.ProductName", "ProductName")
                        .WithMany()
                        .HasForeignKey("ProductNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductName");
                });

            modelBuilder.Entity("PO_Projekt.Models.ProductName", b =>
                {
                    b.HasOne("PO_Projekt.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PO_Projekt.Models.ProductForm", "ProductForm")
                        .WithMany()
                        .HasForeignKey("ProductFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PO_Projekt.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("ProductForm");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("PO_Projekt.Models.ShippingData", b =>
                {
                    b.HasOne("PO_Projekt.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
