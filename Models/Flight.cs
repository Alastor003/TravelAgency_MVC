using System.Diagnostics;
using TravelAgency_MVC;

namespace TravelAgency_MVC.Models
{
    public class Flight
    {
        #region Property
        public int id { get; set; }
        public City origin { get; set; }
        public int originId { get; set; }
        public City destination { get; set; }
        public int destinationId { get; set; }
        public int soldFlights { get; set; }
        public int capacity { get; set; }
        public List<User> passengers { get; set; } = new List<User>();
        public double flightPrice { get; set; }
        public DateTime date { get; set; }
        public string airline { get; set; }
        public string aircraft { get; set; }
        public List<FlightReservation> allFlights { get; set; } = new List<FlightReservation>();
        public List<UsersFlights> usersFlights { get; set; } 

        #endregion

        #region Constructor
        public Flight() { }

        public Flight(int idFlight, City initialOrigin, City finalDestination, int sold, int totalCapacity, 
            double price, DateTime currentDate, string airline, string aircraft)
        {
            id = idFlight;
            origin = initialOrigin;
            destination = finalDestination;
            soldFlights = sold;
            capacity = totalCapacity;
            flightPrice = price;
            date = currentDate;
            this.airline = airline;
            this.aircraft = aircraft;
        }
        #endregion

        public string[] showFlyghts()
        {
            return new string[] { id.ToString(), origin.cityName, destination.cityName, soldFlights.ToString(), capacity.ToString(), flightPrice.ToString(), date.ToString(), airline, aircraft };
        }

       public override string ToString()
        {
            return $"Origen: {origin.cityName}, Destino: {destination.cityName}, Capacidad: {capacity}, Precio: {flightPrice}, Fecha: {date}, Aerolinea: {airline}, Avion: {aircraft}";
        }


    }
}