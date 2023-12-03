using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class UsersHotels
    {
        public int idUser {  get; set; }
        public User user { get; set; }
        public int idHotel { get; set; }
        public Hotel hotel { get; set; }
        public int cantidad { get; set; }
        
        public UsersHotels() {}

     
    }
}
