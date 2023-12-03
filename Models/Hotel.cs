using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TravelAgency_MVC.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public City Location { get; set; }
        public int locationId { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
        public List<User> Hosts { get; set; } = new List<User>();
        public string Name { get; set; }
        public List<HotelReservation> MyReservations { get; set; } = new List<HotelReservation>();
        public List<UsersHotels> usersHotels { get; set; }

        public Hotel()
        {

        }

        public Hotel(int id, City location, int capacity, double price, string name)
        {
            Id = id;
            Location = location;
            Capacity = capacity;
            Price = price;
            Name = name;
        }


        public string[] showHotels()
        {
            return new string[] { Id.ToString(), Name, Location.cityName.ToString(), Hosts.Count.ToString(), Capacity.ToString(), Price.ToString() };
        }

        public override string ToString()
        {
            return $"Ubicacion: {Location.cityName}, Capacidad: {Capacity}, Precio: {Price}, Nombre: {Name}";
        }
    }
}