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
                           .Select(data => new Hotel(data[1], Convert.ToDouble(data[4]), GetCityByName(data[7]), data[3], Convert.ToInt32(data[0])))
                           .ToList();
                // save the list of ojects into the field
                HotelCatalogue = hotels;
            }

        public void AscendingSortHotels(List<Hotel> hotels, Func<Hotel, double> price)
        {
            // create a local copy of the list
            var localItems = new List<Hotel>(hotels);

            // using Parallel.ForEach to sort the items in parallel
            Parallel.ForEach(localItems, hotel =>
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < localItems.Count - 1; i++)
                    {
                        for (int j = 0; j < localItems.Count - i - 1; j++)
                        {
                            // if the price of the current item is greater than the next item
                            if (price(localItems[j]).CompareTo(price(localItems[j + 1])) > 0)
                            {
                                // swapping the elements
                                var temp = localItems[j];
                                localItems[j] = localItems[j + 1];
                                localItems[j + 1] = temp;
                                swapped = true;
                            }
                        }
                    }
                } while (swapped);
            });

            // clear the original list and add the sorted items
            hotels.Clear();
            // add the sorted items to the original list
            hotels.AddRange(localItems);
        }
      }
    }
