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


        // Constructor
        public DataStore()
        {
            HotelCatalogue = new List<Hotel>();
        }

        public void addHotel(Hotel hotel)
        {
            HotelCatalogue.Add(hotel);
        }

    }
}
