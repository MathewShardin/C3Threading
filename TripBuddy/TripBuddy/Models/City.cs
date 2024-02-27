using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    internal class City
    {
        // This class is used to store information about a city and hotel will be placed in a city
        public string Name { get; set; }
        public string Country { get; set; }
        public string Coordinates { get; set; }
        public string Province { get; set; }

        // Constructor
        public City(string name, string country, string coordinates, string province)
        {
            Name = name;
            Country = country;
            Coordinates = coordinates;
            Province = province;
        }



    }
}
