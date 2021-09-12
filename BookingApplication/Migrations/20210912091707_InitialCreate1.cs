using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApplication.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    People = table.Column<int>(type: "int", nullable: false),
                    Beds = table.Column<int>(type: "int", nullable: false),
                    Balcony = table.Column<bool>(type: "bit", nullable: false),
                    PrivateBathroom = table.Column<bool>(type: "bit", nullable: false),
                    Kitchen = table.Column<bool>(type: "bit", nullable: false),
                    Tv = table.Column<bool>(type: "bit", nullable: false),
                    NightPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelPhoto_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentPhoto_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    People = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_HotelId",
                table: "Apartment",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentPhoto_ApartmentId",
                table: "ApartmentPhoto",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelPhoto_HotelId",
                table: "HotelPhoto",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ApartmentId",
                table: "Reservation",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentPhoto");

            migrationBuilder.DropTable(
                name: "HotelPhoto");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Hotel");
        }
    }
}
