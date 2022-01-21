using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PO_Projekt.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingDataId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    RequiresPrescription = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ImageFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductFormId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductNames_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductNames_ProductForms_ProductFormId",
                        column: x => x.ProductFormId,
                        principalTable: "ProductForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductNames_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionCode = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HomeNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    LocalNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    PostalNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingData_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductNameId = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductNames_ProductNameId",
                        column: x => x.ProductNameId,
                        principalTable: "ProductNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionOrders_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pfizer" },
                    { 2, "Novartis" },
                    { 3, "Us Pharmacia" },
                    { 4, "Paso" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "ShippingDataId", "ShippingType", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3, 1 },
                    { 2, new DateTime(2021, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1 },
                    { 3, new DateTime(2021, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2 },
                    { 4, new DateTime(2022, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductForms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Inne" },
                    { 6, "Maść" },
                    { 5, "Kapsułka" },
                    { 7, "Opatrunek" },
                    { 3, "Saszetka" },
                    { 2, "Plaster" },
                    { 1, "Tabletka" },
                    { 4, "Syrop" }
                });

            migrationBuilder.InsertData(
                table: "ProductOrders",
                columns: new[] { "Id", "OrderId", "ProductId" },
                values: new object[,]
                {
                    { 1, 4, 11 },
                    { 2, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "inne" },
                    { 8, "leki" },
                    { 7, "dziecko" },
                    { 6, "przeziębienie" },
                    { 3, "suplementy" },
                    { 4, "przeciwbólowe" },
                    { 2, "kosmetyki" },
                    { 1, "sprzęt medyczny" },
                    { 5, "antybiotyki" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "Password" },
                values: new object[,]
                {
                    { 2, new DateTime(1990, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "korneliusz.smigly@op.pl", "supertajne" },
                    { 1, new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ania.haha@gmail.com", "tajnehaslo" },
                    { 3, new DateTime(1986, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "sowa.minerwa@o2.pl", "illuminati" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "Id", "EndDate", "PrescriptionCode", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 14523, new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, new DateTime(2022, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13778, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1, new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13423, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductNames",
                columns: new[] { "Id", "Description", "ImageFilename", "ManufacturerId", "Name", "Price", "ProductFormId", "ProductTypeId", "RequiresPrescription" },
                values: new object[,]
                {
                    { 6, "To wyrób medyczny, z opatrunkiem do cięcia, który chroni przed zarazkami i zabrudzeniem. Utrzymuje skórę w czystości, nie powoduje uczuleń i nie przywiera do rany. Rozmiar: 1 m x 6 cm.", "plastry.png", 3, "Plaster z opatrunkiem, tkaninowy, 1 m x 6 cm, 1 szt.", 5.9900000000000002, 2, 9, false },
                    { 10, "", "", 4, "Cital, 20 mg, tabletki powlekane, 60 szt.", 36.990000000000002, 1, 8, true },
                    { 9, "Doustnie. Dawkę i częstotliwość przyjmowania preparatu ustala lekarz. Lek należy przyjąć jak najszybciej po wystąpieniu objawów migreny. Jeśli ból nie ustąpił dawkę można powtórzyć po upływie 2 godzin. Tabletki należy połykać w całości, popijając wodą.", "", 3, "Sumatriptan Medical Valley, 50 mg, tabletki, 6 szt.", 25.59, 1, 8, true },
                    { 5, "Produkt leczniczy Ibuprom działa przeciwbólowo, przeciwzapalnie i przeciwgorączkowo. Stosuje się go w bólach głowy, zębów, mięśniowych, okolicy lędźwiowo-krzyżowej, kostnych i stawowych oraz w bolesnym miesiączkowaniu oraz w gorączce.", "", 2, "Ibuprom, 200 mg, tabletki powlekane, 10 szt.", 6.9900000000000002, 1, 8, false },
                    { 1, "Neurologia Psychiatria: nasenne przeciwlękowe przeciwdrgawkowe uspokajające zmniejsza napięcie mięśni", "xanax.jpg", 1, "Xanax", 45.990000000000002, 1, 8, true },
                    { 4, "Ibuprom Max to lek przeciwbólowy, ale także stosuje się go w leczeniu stanu zapalnego. Lek również obniża gorączkę.", "", 1, "Ibuprom Max, 400 mg, tabletki drażowane, 48 szt. (butelka)", 26.489999999999998, 1, 8, false },
                    { 8, "Fervex Junior to lek w postaci granulatu do sporządzania roztworu doustnego dla dzieci od 6 roku życia. Ma działanie przeciwbólowe i przeciwgorączkowe, udrażnia przewody nosowe, hamuje odruch kichania i łzawienie oczu, uzupełnia niedobory witaminy C.", "fervex.png", 1, "Fervex Junior, granulat bez cukru, 8 saszetek", 19.190000000000001, 3, 6, false },
                    { 2, "Lek przeciwbólowy i przeciwgorączkowy, który jako substancję czynną zawiera paracetamol. Lek stosuje się w bólach różnego pochodzenia, zarówno głowy, zębów, mięśni jak i menstruacyjnych, kostno-stawowych czy nerwobólach.", "apap.jpg", 3, "Apap", 6.9900000000000002, 1, 6, false },
                    { 14, "Polibiotic to produkt leczniczy zawierający 3 antybiotyki działające w przypadku zakażeń skórnych, maść do stosowania w przypadku ran, owrzodzeń i oparzeń.", "m_antybiotyk_2.png", 1, "Polibiotic, (5 mg+5000 j.m.+400 j.m.g), maść, 15 g (tuba)", 11.890000000000001, 6, 5, true },
                    { 13, "Tribiotic to produkt leczniczy zawierający 3 antybiotyki, które wykazują działanie przeciwbakteryjne. Lek do stosowania w przypadku zadrapań, ran, oparzeń.", "m_antybiotyk.png", 4, "Tribiotic", 16.59, 6, 5, true },
                    { 12, "InVag to produkt leczniczy zawierający bakterie kwasy mlekowego, które przywracają równowagę pH pochwy do stosowania w zaburzeń mikroflory spowodowanej m.in. stosowaniem antybiotyków czy zmianami hormonalnymi.", "p_antybiotyk.png", 2, "InVag, kapsułki twarde, dopochwowe, 7 szt.", 34.990000000000002, 1, 5, true },
                    { 15, "suplement diety wspomagający układ odpornościowy. Produkt przeznaczony dla osób dorosłych.", "witamina_c.png", 2, "Witamina C 1000 mg Forte, tabletki do ssania, smak pomarańczowy, 60 szt.", 14.99, 1, 3, false },
                    { 11, "suplement diety w formie płynu, który uzupełnia codzienną dietę w magnez i witaminę B6. Produkt przeznaczony dla dzieci od 3 roku życia i osób dorosłych.", "syrop.png", 3, "JuniorMag, płyn, smak truskawkowy, 120 ml", 13.99, 4, 3, false },
                    { 7, "Kremowy szampon i płyn do kąpieli – w łagodny sposób myje oraz pielęgnuje skórę i włosy dziecka. Kosmetyk może być stosowany już od pierwszego dnia życia maluszka. Szampon w swoim składzie zawiera między innymi: bisabolol, który łagodzi podrażnienia.", "baby_care.png", 4, "Pikabu Baby Care, kremowy szampon i płyn do kąpieli, 300 ml", 24.989999999999998, 8, 7, false },
                    { 3, "To wyrób medyczny, wielokrotnego użytku. Produkt może być stosowany jako opaska podtrzymująca opatrunki, uciskowa oraz usztywniająca okolice okołostawowe. Długość opaski po relaksacji wynosi nie mniej niż 1,5 m.", "opaska-elastyczna-tkana-z-zapinka.jpg", 4, "Opaska elastyczna z zapinką", 3.9900000000000002, 7, 1, false }
                });

            migrationBuilder.InsertData(
                table: "ShippingData",
                columns: new[] { "Id", "City", "HomeNumber", "LocalNumber", "PostalNumber", "Street", "UserId" },
                values: new object[,]
                {
                    { 3, "Kraków", "29", "4", 51753, "Łańcuchowa", 2 },
                    { 1, "Wrocław", "23", "1", 50243, "Fiołkowa", 1 },
                    { 2, "Wrocław", "2", null, 50243, "Nizinna", 1 },
                    { 4, "Warszawa", "145", "10", 60321, "Długa", 1 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionOrders",
                columns: new[] { "Id", "OrderId", "PrescriptionId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ExpirationDate", "ProductNameId" },
                values: new object[,]
                {
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2022, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, new DateTime(2022, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(2022, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 1, new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2022, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2022, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionOrders_OrderId",
                table: "PrescriptionOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionOrders_PrescriptionId",
                table: "PrescriptionOrders",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_UserId",
                table: "Prescriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNames_ManufacturerId",
                table: "ProductNames",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNames_ProductFormId",
                table: "ProductNames",
                column: "ProductFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductNames_ProductTypeId",
                table: "ProductNames",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductNameId",
                table: "Products",
                column: "ProductNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingData_UserId",
                table: "ShippingData",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionOrders");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShippingData");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "ProductNames");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "ProductForms");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
