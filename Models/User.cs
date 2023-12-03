using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class User
    {
        public int idUser { get; set; }
        public int dni { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int failedTries { get; set; }
        public bool lockedUser { get; set; }
        public double credit { get; set; }
        public bool isAdmin { get; set; }
        public List<HotelReservation> myHotelBookings { get; set; } = new List<HotelReservation>();
        public List<FlightReservation> myFlightBookings { get; set; } = new List<FlightReservation>();
        public List<Hotel> historyHotelBookings { get; set; } = new List<Hotel>();
        public List<Flight> historyFlightBookings { get; set; } = new List<Flight>();
        public List<UsersFlights> usersFlights { get; set; }
        public List<UsersHotels> usersHotels { get; set; }


        public List<FlightReservation> obtainFlightBookings()
        {
            return myFlightBookings.ToList();
        }

        public List<HotelReservation> obtainHotelBookings()
        {
            return myHotelBookings.ToList();
        }

        public User()
        {
        }

        public User(string Email, string Password)
        {
            email = Email;
            password = Password;
        }

        public User(int IdUser, int Dni, string Name, string Surname, string Email, string Password, bool IsAdmin)
        {

            idUser = IdUser;
            dni = Dni;
            name = Name;
            surname = Surname;
            email = Email;
            password = Password;
            isAdmin = IsAdmin;
            credit = 0;
            failedTries = 0;
            lockedUser = false;
            this.historyHotelBookings = new List<Hotel>();
            this.historyFlightBookings = new List<Flight>();
            this.myFlightBookings = new List<FlightReservation>();
            this.myHotelBookings = new List<HotelReservation>();
        }
        public User(int dni, string name, string surname, string email, string password)
        {
            this.dni = dni;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.password = password;
            this.isAdmin = false;
            this.credit = 0;
            this.failedTries = 0;
            this.lockedUser = false;
        }

        public double DepositCredit(double credit)
        {
            return this.credit += credit;
        }
        public string[] showUsers()
        {
            return new string[] { idUser.ToString(), name, surname, dni.ToString(), email, password, failedTries.ToString(), credit.ToString(), isAdmin.ToString(), lockedUser.ToString()};
        }

    }
}