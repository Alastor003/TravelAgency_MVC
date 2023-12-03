using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency_MVC.Models
{
    public class City
    {
        #region Property
        public int id { get; set; }
        public string cityName { get; set; }
        public List<Flight> flights { get; set; } = new List<Flight>();
        public List<Flight> flightsDestinacion { get; set; } = new List<Flight>();
        public List<Hotel> hotels { get; set; } = new List<Hotel>();
        #endregion

        #region Constructor

        public City() { }

        public City(string CityName)
        {
            cityName = CityName;
        }

        #endregion

        public string[] showCities()
        {
            return new string[] { id.ToString(), cityName};
        }

    }
}
