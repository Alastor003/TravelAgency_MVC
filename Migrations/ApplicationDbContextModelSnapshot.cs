﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency_MVC.Models;

#nullable disable

namespace TravelAgency_MVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelAgency_MVC.Models.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("cityName")
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.ToTable("City", (string)null);

                    b.HasData(
                        new
                        {
                            id = 1,
                            cityName = "New York"
                        },
                        new
                        {
                            id = 2,
                            cityName = "Paris"
                        });
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Flight", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("aircraft")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("airline")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime");

                    b.Property<int>("destinationId")
                        .HasColumnType("int");

                    b.Property<double>("flightPrice")
                        .HasColumnType("float");

                    b.Property<int>("originId")
                        .HasColumnType("int");

                    b.Property<int>("soldFlights")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("destinationId");

                    b.HasIndex("originId");

                    b.ToTable("flights", (string)null);

                    b.HasData(
                        new
                        {
                            id = 1,
                            aircraft = "A380",
                            airline = "Airline1",
                            capacity = 150,
                            date = new DateTime(2024, 1, 11, 15, 47, 54, 181, DateTimeKind.Local).AddTicks(5664),
                            destinationId = 2,
                            flightPrice = 300.0,
                            originId = 1,
                            soldFlights = 50
                        },
                        new
                        {
                            id = 2,
                            aircraft = "B747",
                            airline = "Airline2",
                            capacity = 120,
                            date = new DateTime(2024, 1, 26, 15, 47, 54, 181, DateTimeKind.Local).AddTicks(5678),
                            destinationId = 1,
                            flightPrice = 250.0,
                            originId = 2,
                            soldFlights = 30
                        });
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.FlightReservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<double>("amountPaid")
                        .HasColumnType("float");

                    b.Property<int>("myFlightId")
                        .HasColumnType("int");

                    b.Property<int>("myUserId")
                        .HasColumnType("int");

                    b.Property<int>("sites")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("myFlightId");

                    b.HasIndex("myUserId");

                    b.ToTable("flight_reservation", (string)null);
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("locationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("locationId");

                    b.ToTable("hotels", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 100,
                            Name = "Grand Hotel",
                            Price = 150.0,
                            locationId = 1
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 80,
                            Name = "Eiffel Tower Inn",
                            Price = 120.0,
                            locationId = 2
                        });
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.HotelReservation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<DateTime>("Since")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Until")
                        .HasColumnType("datetime");

                    b.Property<int>("myHotelId")
                        .HasColumnType("int");

                    b.Property<int>("myUserId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("myHotelId");

                    b.HasIndex("myUserId");

                    b.ToTable("hotel_reservation", (string)null);
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUser"));

                    b.Property<double>("credit")
                        .HasColumnType("float");

                    b.Property<int>("dni")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("failedTries")
                        .HasColumnType("int");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("lockedUser")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("idUser");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            idUser = 1,
                            credit = 1000.0,
                            dni = 12345678,
                            email = "john@gmail.com",
                            failedTries = 0,
                            isAdmin = false,
                            lockedUser = false,
                            name = "John",
                            password = "123",
                            surname = "Doe"
                        },
                        new
                        {
                            idUser = 2,
                            credit = 1500.0,
                            dni = 87654321,
                            email = "admin@admin.com",
                            failedTries = 0,
                            isAdmin = true,
                            lockedUser = false,
                            name = "Jane",
                            password = "admin",
                            surname = "Smith"
                        });
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.UsersFlights", b =>
                {
                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.Property<int>("idFlight")
                        .HasColumnType("int");

                    b.HasKey("idUser", "idFlight");

                    b.HasIndex("idFlight");

                    b.ToTable("usersFlights");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.UsersHotels", b =>
                {
                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.Property<int>("idHotel")
                        .HasColumnType("int");

                    b.Property<int>("cantidad")
                        .HasColumnType("int");

                    b.HasKey("idUser", "idHotel");

                    b.HasIndex("idHotel");

                    b.ToTable("usersHotels");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Flight", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.City", "destination")
                        .WithMany("flightsDestinacion")
                        .HasForeignKey("destinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency_MVC.Models.City", "origin")
                        .WithMany("flights")
                        .HasForeignKey("originId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("destination");

                    b.Navigation("origin");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.FlightReservation", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.Flight", "myFlight")
                        .WithMany("allFlights")
                        .HasForeignKey("myFlightId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TravelAgency_MVC.Models.User", "myUser")
                        .WithMany("myFlightBookings")
                        .HasForeignKey("myUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("myFlight");

                    b.Navigation("myUser");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Hotel", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.City", "Location")
                        .WithMany("hotels")
                        .HasForeignKey("locationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.HotelReservation", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.Hotel", "MyHotel")
                        .WithMany("MyReservations")
                        .HasForeignKey("myHotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency_MVC.Models.User", "MyUser")
                        .WithMany("myHotelBookings")
                        .HasForeignKey("myUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyHotel");

                    b.Navigation("MyUser");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.UsersFlights", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.Flight", "flight")
                        .WithMany("usersFlights")
                        .HasForeignKey("idFlight")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency_MVC.Models.User", "user")
                        .WithMany("usersFlights")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("flight");

                    b.Navigation("user");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.UsersHotels", b =>
                {
                    b.HasOne("TravelAgency_MVC.Models.Hotel", "hotel")
                        .WithMany("usersHotels")
                        .HasForeignKey("idHotel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency_MVC.Models.User", "user")
                        .WithMany("usersHotels")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");

                    b.Navigation("user");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.City", b =>
                {
                    b.Navigation("flights");

                    b.Navigation("flightsDestinacion");

                    b.Navigation("hotels");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Flight", b =>
                {
                    b.Navigation("allFlights");

                    b.Navigation("usersFlights");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.Hotel", b =>
                {
                    b.Navigation("MyReservations");

                    b.Navigation("usersHotels");
                });

            modelBuilder.Entity("TravelAgency_MVC.Models.User", b =>
                {
                    b.Navigation("myFlightBookings");

                    b.Navigation("myHotelBookings");

                    b.Navigation("usersFlights");

                    b.Navigation("usersHotels");
                });
#pragma warning restore 612, 618
        }
    }
}
