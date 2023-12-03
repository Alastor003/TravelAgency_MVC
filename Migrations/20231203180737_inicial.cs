using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgency_MVC.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    surname = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    failedTries = table.Column<int>(type: "int", nullable: false),
                    lockedUser = table.Column<bool>(type: "bit", nullable: false),
                    credit = table.Column<double>(type: "float", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    originId = table.Column<int>(type: "int", nullable: false),
                    destinationId = table.Column<int>(type: "int", nullable: false),
                    soldFlights = table.Column<int>(type: "int", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    flightPrice = table.Column<double>(type: "float", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    airline = table.Column<string>(type: "varchar(50)", nullable: false),
                    aircraft = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_flights_City_destinationId",
                        column: x => x.destinationId,
                        principalTable: "City",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_City_originId",
                        column: x => x.originId,
                        principalTable: "City",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locationId = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotels_City_locationId",
                        column: x => x.locationId,
                        principalTable: "City",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flight_reservation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    myUserId = table.Column<int>(type: "int", nullable: false),
                    myFlightId = table.Column<int>(type: "int", nullable: false),
                    amountPaid = table.Column<double>(type: "float", nullable: false),
                    sites = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flight_reservation", x => x.id);
                    table.ForeignKey(
                        name: "FK_flight_reservation_flights_myFlightId",
                        column: x => x.myFlightId,
                        principalTable: "flights",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flight_reservation_users_myUserId",
                        column: x => x.myUserId,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersFlights",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idFlight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersFlights", x => new { x.idUser, x.idFlight });
                    table.ForeignKey(
                        name: "FK_usersFlights_flights_idFlight",
                        column: x => x.idFlight,
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usersFlights_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotel_reservation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    myHotelId = table.Column<int>(type: "int", nullable: false),
                    myUserId = table.Column<int>(type: "int", nullable: false),
                    Since = table.Column<DateTime>(type: "datetime", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime", nullable: false),
                    AmountPaid = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel_reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_hotel_reservation_hotels_myHotelId",
                        column: x => x.myHotelId,
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hotel_reservation_users_myUserId",
                        column: x => x.myUserId,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usersHotels",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false),
                    idHotel = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usersHotels", x => new { x.idUser, x.idHotel });
                    table.ForeignKey(
                        name: "FK_usersHotels_hotels_idHotel",
                        column: x => x.idHotel,
                        principalTable: "hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usersHotels_users_idUser",
                        column: x => x.idUser,
                        principalTable: "users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "id", "cityName" },
                values: new object[,]
                {
                    { 1, "New York" },
                    { 2, "Paris" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "idUser", "credit", "dni", "email", "failedTries", "isAdmin", "lockedUser", "name", "password", "surname" },
                values: new object[,]
                {
                    { 1, 1000.0, 12345678, "john@gmail.com", 0, false, false, "John", "123", "Doe" },
                    { 2, 1500.0, 87654321, "admin@admin.com", 0, true, false, "Jane", "admin", "Smith" }
                });

            migrationBuilder.InsertData(
                table: "flights",
                columns: new[] { "id", "aircraft", "airline", "capacity", "date", "destinationId", "flightPrice", "originId", "soldFlights" },
                values: new object[,]
                {
                    { 1, "A380", "Airline1", 150, new DateTime(2024, 1, 2, 15, 7, 37, 356, DateTimeKind.Local).AddTicks(3186), 2, 300.0, 1, 50 },
                    { 2, "B747", "Airline2", 120, new DateTime(2024, 1, 17, 15, 7, 37, 356, DateTimeKind.Local).AddTicks(3200), 1, 250.0, 2, 30 }
                });

            migrationBuilder.InsertData(
                table: "hotels",
                columns: new[] { "Id", "Capacity", "Name", "Price", "locationId" },
                values: new object[,]
                {
                    { 1, 100, "Grand Hotel", 150.0, 1 },
                    { 2, 80, "Eiffel Tower Inn", 120.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_flight_reservation_myFlightId",
                table: "flight_reservation",
                column: "myFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_flight_reservation_myUserId",
                table: "flight_reservation",
                column: "myUserId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_destinationId",
                table: "flights",
                column: "destinationId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_originId",
                table: "flights",
                column: "originId");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_reservation_myHotelId",
                table: "hotel_reservation",
                column: "myHotelId");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_reservation_myUserId",
                table: "hotel_reservation",
                column: "myUserId");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_locationId",
                table: "hotels",
                column: "locationId");

            migrationBuilder.CreateIndex(
                name: "IX_usersFlights_idFlight",
                table: "usersFlights",
                column: "idFlight");

            migrationBuilder.CreateIndex(
                name: "IX_usersHotels_idHotel",
                table: "usersHotels",
                column: "idHotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flight_reservation");

            migrationBuilder.DropTable(
                name: "hotel_reservation");

            migrationBuilder.DropTable(
                name: "usersFlights");

            migrationBuilder.DropTable(
                name: "usersHotels");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
