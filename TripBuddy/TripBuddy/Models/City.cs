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
        public string Coordinates { get; set; } // Format Lat:lng

        // Constructor
        public City(string name, string country, string coordinates)
        {
            Name = name;
            Country = country;
            Coordinates = coordinates;
        }

        // Get City info based on csv of cities
        public City (string name)
        {
            Name = name;
            string[] infoTemp = CsvAccessor.GetCityInfo(Name);
            if (infoTemp != null)
            {
                Country = infoTemp[4];
                Coordinates = infoTemp[2] + ":" + infoTemp[3];
            } else // Input default Values
            {
                Country = "Netherlands";
                Coordinates = "52.3728:4.8936"; // AMS lat and long
                   
            }
        }



    }
}
