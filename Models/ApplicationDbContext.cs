using Microsoft.EntityFrameworkCore;



namespace TravelAgency_MVC.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> users { get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Flight> flights { get; set; }
        public DbSet<City> cities { get; set; } 
        public DbSet<FlightReservation> flightsReservation { get; set; }
        public DbSet<HotelReservation> hotelReservations { get; set; }
        public DbSet<UsersHotels> usersHotels { get; set; }
        public DbSet<UsersFlights> usersFlights { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optionsBuilder) : base(optionsBuilder) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Context");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabla usuarios
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasKey(u => u.idUser);
            //Properties de la tabla User
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.dni).HasColumnType("int");
                user.Property(u => u.dni).IsRequired(true);
                user.Property(u => u.name).HasColumnType("varchar(50)");
                user.Property(u => u.name).IsRequired(true);
                user.Property(u => u.surname).HasColumnType("varchar(50)");
                user.Property(u => u.surname).IsRequired(true);
                user.Property(u => u.email).HasColumnType("varchar(50)");
                user.Property(u => u.email).IsRequired(true);
                user.Property(u => u.password).HasColumnType("varchar(50)");
                user.Property(u => u.password).IsRequired(true);
                user.Property(u => u.failedTries).HasColumnType("int");
                user.Property(u => u.failedTries).IsRequired(true);
                user.Property(u => u.lockedUser).HasColumnType("bit");
                user.Property(u => u.lockedUser).IsRequired(true);
                user.Property(u => u.credit).HasColumnType("float");
                user.Property(u => u.credit).IsRequired(true);
                user.Property(u => u.isAdmin).HasColumnType("bit");
                user.Property(u => u.isAdmin).IsRequired(true);
            });

            //Tabla city
            modelBuilder.Entity<City>()
                .ToTable("City")
                .HasKey(c => c.id);

            //Properties de la tabla City
            modelBuilder.Entity<City>(city =>
            {
                city.Property(u => u.cityName).HasColumnType("varchar(50)");
            });

            //Tabla hotel
            modelBuilder.Entity<Hotel>()
                .ToTable("hotels")
                .HasKey(h => h.Id);
            //Properties de la tabla Hotel
            modelBuilder.Entity<Hotel>(hotel =>
            {
                hotel.Property(u => u.locationId).HasColumnType("int");
                hotel.Property(u => u.locationId).IsRequired(true);
                hotel.Property(u => u.Capacity).HasColumnType("int");
                hotel.Property(u => u.Capacity).IsRequired(true);
                hotel.Property(u => u.Price).HasColumnType("float");
                hotel.Property(u => u.Price).IsRequired(true);
                hotel.Property(u => u.Name).HasColumnType("varchar(50)");
                hotel.Property(u => u.Name).IsRequired(true);
            });


            //Tabla flight
            modelBuilder.Entity<Flight>()
                .ToTable("flights")
                .HasKey(f => f.id);

            //Properties de la tabla Flight
            modelBuilder.Entity<Flight>(flight =>
            {
                flight.Property(u => u.originId).HasColumnType("int");
                flight.Property(u => u.originId).IsRequired(true);
                flight.Property(u => u.destinationId).HasColumnType("int");
                flight.Property(u => u.destinationId).IsRequired(true);
                flight.Property(u => u.soldFlights).HasColumnType("int");
                flight.Property(u => u.soldFlights).IsRequired(true);
                flight.Property(u => u.capacity).HasColumnType("int");
                flight.Property(u => u.capacity).IsRequired(true);
                flight.Property(u => u.flightPrice).HasColumnType("float");
                flight.Property(u => u.flightPrice).IsRequired(true);
                flight.Property(u => u.date).HasColumnType("datetime");
                flight.Property(u => u.date).IsRequired(true);
                flight.Property(u => u.airline).HasColumnType("varchar(50)");
                flight.Property(u => u.airline).IsRequired(true);
                flight.Property(u => u.aircraft).HasColumnType("varchar(50)");
                flight.Property(u => u.aircraft).IsRequired(true);
            });



            //Tabla flightReservation
            modelBuilder.Entity<FlightReservation>()
                .ToTable("flight_reservation")
                .HasKey(f => f.id);

            //Properties de la tabla FlightReservation
            modelBuilder.Entity<FlightReservation>(flightReservarion =>
            {
                flightReservarion.Property(u => u.myUserId).HasColumnType("int");
                flightReservarion.Property(u => u.myUserId).IsRequired(true);
                flightReservarion.Property(u => u.myFlightId).HasColumnType("int");
                flightReservarion.Property(u => u.myFlightId).IsRequired(true);
                flightReservarion.Property(u => u.amountPaid).HasColumnType("float");
                flightReservarion.Property(u => u.amountPaid).IsRequired(true);
                flightReservarion.Property(u => u.sites).HasColumnType("int");
                flightReservarion.Property(u => u.sites).IsRequired(true);
            });


            //Tabla hotelReservation
            modelBuilder.Entity<HotelReservation>()
                .ToTable("hotel_reservation")
                .HasKey(h => h.ID);

            //Properties de la tabla FlightReservation
            modelBuilder.Entity<HotelReservation>(hotelReservarion =>
            {
                hotelReservarion.Property(u => u.myHotelId).HasColumnType("int");
                hotelReservarion.Property(u => u.myHotelId).IsRequired(true);
                hotelReservarion.Property(u => u.myUserId).HasColumnType("int");
                hotelReservarion.Property(u => u.myUserId).IsRequired(true);
                hotelReservarion.Property(u => u.Since).HasColumnType("datetime");
                hotelReservarion.Property(u => u.Since).IsRequired(true);
                hotelReservarion.Property(u => u.Until).HasColumnType("datetime");
                hotelReservarion.Property(u => u.Until).IsRequired(true);
                hotelReservarion.Property(u => u.AmountPaid).HasColumnType("float");
                hotelReservarion.Property(u => u.AmountPaid).IsRequired(true);
                hotelReservarion.Property(u => u.quantity).HasColumnType("int");
                hotelReservarion.Property(u => u.quantity).IsRequired(true);
            });
           
            // RELACIONES
            modelBuilder.Entity<HotelReservation>()
                .HasOne(rh => rh.MyUser)
                .WithMany(u => u.myHotelBookings)
                .HasForeignKey(rh => rh.myUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelReservation>()
                .HasOne(rh => rh.MyHotel)
                .WithMany(u => u.MyReservations)
                .HasForeignKey(rh => rh.myHotelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlightReservation>()
                .HasOne(fr => fr.myUser)
                .WithMany(u => u.myFlightBookings)
                .HasForeignKey(fr => fr.myUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FlightReservation>()
                .HasOne(fr => fr.myFlight)
                .WithMany(f => f.allFlights)
                .HasForeignKey(fr => fr.myFlightId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.origin)
                .WithMany(c =>  c.flights)
                .HasForeignKey(f => f.originId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.destination)
                .WithMany(c => c.flightsDestinacion)
                .HasForeignKey(f => f.destinationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Location)
                .WithMany(c => c.hotels)
                .HasForeignKey(h => h.locationId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.historyHotelBookings)
                .WithMany(h => h.Hosts)
                .UsingEntity<UsersHotels>(
                    euh => euh.HasOne(uh => uh.hotel).WithMany(h => h.usersHotels).HasForeignKey(uh => uh.idHotel),
                    euh => euh.HasOne(uh => uh.user).WithMany(u => u.usersHotels).HasForeignKey(uh => uh.idUser),
                    euh => euh.HasKey(k => new{ k.idUser, k.idHotel })
                );

            modelBuilder.Entity<User>()
                .HasMany(u => u.historyFlightBookings)
                .WithMany(f => f.passengers)
                .UsingEntity<UsersFlights>(
                    euf => euf.HasOne(uf => uf.flight).WithMany(f => f.usersFlights).HasForeignKey(uf => uf.idFlight),
                    euf => euf.HasOne(uf => uf.user).WithMany(u => u.usersFlights).HasForeignKey(uf => uf.idUser),
                    euf => euf.HasKey(k => new { k.idUser, k.idFlight})
                );

             // Datos de prueba usuarios
            modelBuilder.Entity<User>().HasData(
               new User { idUser = 1, dni = 12345678, name = "John", surname = "Doe", email = "john@gmail.com", password = "123", failedTries = 0, lockedUser = false, credit = 1000.0, isAdmin = false },
               new User { idUser = 2, dni = 87654321, name = "Jane", surname = "Smith", email = "admin@admin.com", password = "admin", failedTries = 0, lockedUser = false, credit = 1500.0, isAdmin = true }
           );

            // Datos de prueba para ciudades
            modelBuilder.Entity<City>().HasData(
                new City { id = 1, cityName = "New York" },
                new City { id = 2, cityName = "Paris" }
            );

            // Datos de prueba para hoteles
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, locationId = 1, Capacity = 100, Price = 150.0, Name = "Grand Hotel" },
                new Hotel { Id = 2, locationId = 2, Capacity = 80, Price = 120.0, Name = "Eiffel Tower Inn" }
            );

            // Datos de prueba para vuelos
            modelBuilder.Entity<Flight>().HasData(
                new Flight { id = 1, originId = 1, destinationId = 2, soldFlights = 50, capacity = 150, flightPrice = 300.0, date = DateTime.Now.AddDays(30), airline = "Airline1", aircraft = "A380" },
                new Flight { id = 2, originId = 2, destinationId = 1, soldFlights = 30, capacity = 120, flightPrice = 250.0, date = DateTime.Now.AddDays(45), airline = "Airline2", aircraft = "B747" }
            );

        }  
    }
}
