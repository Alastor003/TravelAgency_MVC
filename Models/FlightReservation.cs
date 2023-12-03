using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class FlightReservation
    {
        public int id {  get; set; }
        public int myUserId { get; set; }
        public int myFlightId { get; set; }
        public Flight myFlight { get; set; }
        public User myUser { get; set; }
        public double amountPaid { get; set; }
        public int sites { get; set; }


        public FlightReservation() 
        {
        }
        public FlightReservation(Flight flight, User user, double amount, int site)
        {
            myFlight = flight;
            myUser = user;
            amountPaid = amount;
            sites = site;
        }

        public FlightReservation(Flight flight, int user, double amount, int site)
        {
            myFlight = flight;
            myUserId = user;
            amountPaid = amount;

        }

        public string[] showFlightBooking()
        {
            //Decidir que datos vamos a cambiar
            return new string[] { myFlight.airline, myUser.name, sites.ToString(), amountPaid.ToString(), id.ToString() };

        }

        public override string ToString()
        {
            return $"Reserva: Vuelo: {myFlight.aircraft}, Usuario: {myUser.name}, Monto que pago: {amountPaid}, Cantidad de lugares: {sites}";
        }
    }
}
