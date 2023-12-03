using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class HotelReservation
    {
        public int ID { get; set; }
        public Hotel MyHotel { get; set; }
        public int myHotelId { get; set; }
        public User MyUser { get; set; }
        public int myUserId { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
        public double AmountPaid { get; set; }
        public int quantity {  get; set; }

        public HotelReservation()
        {
        }
        public HotelReservation(Hotel myHotel, DateTime since, DateTime until)
        {
            MyHotel = myHotel;
            Since = since;
            Until = until;
        }

        public HotelReservation(Hotel myHotel, User myUser, DateTime since, DateTime until, double amountPaid)
        {
            MyHotel = myHotel;
            MyUser = myUser;
            Since = since;
            Until = until;
            AmountPaid = amountPaid;
        }
        public HotelReservation(int id, Hotel myHotel, User myUser, DateTime since, DateTime until, double amountPaid)
        {
            ID = id;
            MyHotel = myHotel;
            MyUser = myUser;
            Since = since;
            Until = until;
            AmountPaid = amountPaid;
        }
        public string[] showHotelBooking()
        {
            return new string[] { MyHotel.Name, MyUser.name, Since.ToString(), Until.ToString(), AmountPaid.ToString(),ID.ToString()};
        
        }
        public override string ToString()
        {
            return $"Hotel Reservation: Hotel: {MyHotel.Name}, Cliente: {MyUser.name}, Desde: {Since}, Hasta: {Until}, Monto Pagado: {AmountPaid}";
        }
    }
}
