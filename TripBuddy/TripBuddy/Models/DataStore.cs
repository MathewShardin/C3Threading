using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    internal class DataStore
    {
        // This class contains hotel information from the csv file

        public List<Hotel> HotelCatalogue { get; set; }
        public List<City> CityCatalogue { get; set; }


        // Constructor
        public DataStore()
        {
            HotelCatalogue = new List<Hotel>();
            CityCatalogue = new List<City>();
        }

        public void AddHotel(Hotel hotel)
        {
            HotelCatalogue.Add(hotel);
        }
        public void AddCity(City cityInp)
        {
            CityCatalogue.Add(cityInp);
        }
        public City GetCityByName(string cityName)
        {
            //TODO MAKE THIS MORE EFFICIENT
            // Find the city in the list
            foreach (City entry in CityCatalogue)
            {
                if (entry.Name == cityName) { return entry; }
            }

            // If it doesn't exist make a new City object and save it
            City cityTemp = new City(cityName);
            AddCity(cityTemp);
            // Return last inserted element
            return CityCatalogue.Last();
        }

        public void ParseFromCsv()
        {
            // read the csv file
            List<string[]> csventries = CsvAccessor.ReadCsvFile();
            // parse the data into custom objects type hotel
            List<Hotel> hotels = csventries.AsParallel().Skip(1)
                       .Select(data => new Hotel(Convert.ToString(data[1]), Convert.ToDouble(data[4]), GetCityByName(data[7]), Convert.ToString(data[3]), Convert.ToInt16(data[0])))
                       .ToList();
            // save the list of ojects into the field
            HotelCatalogue = hotels;
        }

    }
}
