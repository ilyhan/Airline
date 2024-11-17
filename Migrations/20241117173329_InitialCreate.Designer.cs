﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Airline.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241117173329_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Airline.Models.Aircraft", b =>
                {
                    b.Property<int>("AircraftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AircraftId"));

                    b.Property<int>("OperationalPeriod")
                        .HasColumnType("int");

                    b.Property<string>("TailNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("YearOfManufacture")
                        .HasColumnType("int");

                    b.HasKey("AircraftId");

                    b.HasIndex("TypeId");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("Airline.Models.AircraftType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CruiseSpeed")
                        .HasColumnType("int");

                    b.Property<int>("MaxFlightAltitude")
                        .HasColumnType("int");

                    b.Property<int>("MaxTakeoffWeight")
                        .HasColumnType("int");

                    b.Property<int>("PassengerCapacityBusiness")
                        .HasColumnType("int");

                    b.Property<int>("PassengerCapacityEconomy")
                        .HasColumnType("int");

                    b.Property<int>("Range")
                        .HasColumnType("int");

                    b.HasKey("TypeId");

                    b.ToTable("AircraftTypes");
                });

            modelBuilder.Entity("Airline.Models.Crew", b =>
                {
                    b.Property<int>("CrewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrewId"));

                    b.Property<string>("FlightZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CrewId");

                    b.ToTable("Crews");
                });

            modelBuilder.Entity("Airline.Models.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DestinationId"));

                    b.Property<string>("Airport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("Airline.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("AddressApartment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressHouse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressStreet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CrewId")
                        .HasColumnType("int");

                    b.Property<int>("ExperienceYears")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CrewId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Airline.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<int>("AircraftId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ArrivalDestinationId")
                        .HasColumnType("int");

                    b.Property<int>("CrewId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDatetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureDestinationId")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.HasIndex("AircraftId");

                    b.HasIndex("ArrivalDestinationId");

                    b.HasIndex("CrewId");

                    b.HasIndex("DepartureDestinationId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Airline.Models.Passenger", b =>
                {
                    b.Property<int>("PassengerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassengerId"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PassengerId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("Airline.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketId"));

                    b.Property<string>("BaggageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BaggageWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<int>("PassengerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RowNumber")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.HasKey("TicketId");

                    b.HasIndex("FlightId");

                    b.HasIndex("PassengerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Airline.Models.Aircraft", b =>
                {
                    b.HasOne("Airline.Models.AircraftType", "Type")
                        .WithMany("Aircrafts")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Airline.Models.Employee", b =>
                {
                    b.HasOne("Airline.Models.Crew", "Crew")
                        .WithMany("Employees")
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crew");
                });

            modelBuilder.Entity("Airline.Models.Flight", b =>
                {
                    b.HasOne("Airline.Models.Aircraft", "Aircraft")
                        .WithMany("Flights")
                        .HasForeignKey("AircraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.Models.Destination", "ArrivalDestination")
                        .WithMany("ArrivalFlights")
                        .HasForeignKey("ArrivalDestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Airline.Models.Crew", "Crew")
                        .WithMany("Flights")
                        .HasForeignKey("CrewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.Models.Destination", "DepartureDestination")
                        .WithMany("DepartureFlights")
                        .HasForeignKey("DepartureDestinationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Aircraft");

                    b.Navigation("ArrivalDestination");

                    b.Navigation("Crew");

                    b.Navigation("DepartureDestination");
                });

            modelBuilder.Entity("Airline.Models.Ticket", b =>
                {
                    b.HasOne("Airline.Models.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Airline.Models.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");

                    b.Navigation("Passenger");
                });

            modelBuilder.Entity("Airline.Models.Aircraft", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("Airline.Models.AircraftType", b =>
                {
                    b.Navigation("Aircrafts");
                });

            modelBuilder.Entity("Airline.Models.Crew", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Flights");
                });

            modelBuilder.Entity("Airline.Models.Destination", b =>
                {
                    b.Navigation("ArrivalFlights");

                    b.Navigation("DepartureFlights");
                });

            modelBuilder.Entity("Airline.Models.Flight", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("Airline.Models.Passenger", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
