using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class UsersFlights
    {
        public int idUser { get; set; }
        public User user { get; set; }
        public int idFlight { get; set; }
        public Flight flight { get; set; }

        public UsersFlights() { }
    }
}
