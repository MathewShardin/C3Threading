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


        // Constructor
        public DataStore()
        {
            HotelCatalogue = new List<Hotel>();
        }

        public void AddHotel(Hotel hotel)
        {
            HotelCatalogue.Add(hotel);
        }

        public void ParseFromCsv()
        {
            // Read the CSV file
            List<string[]> csvEntries = CsvAccessor.ReadCsvFile();
            // Parse the data into custom objects type Hotel
            var customers = csvEntries.Skip(1) // Skip the header row
                                 .AsParallel()
                                 .Select(line =>
                                 {
                                     Hotel tempHotel = new Hotel(line[1], line[])
                                 });


        }

    }
}
