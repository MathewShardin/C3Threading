using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    public class Trip
    {

        // This class is used to store information about a trip

        public double TotalPrice { get; set; }
        public List<LocationStop> Stops { get; set; }


        // Constructor
        public Trip()
        {
            TotalPrice = 0;
            Stops = new List<LocationStop>();
        }

        public void calculateTotalPrice()
        {
            double sum = 0;
            foreach (LocationStop stop in Stops)
            {
                sum += stop.Hotel.Price;
            }

            this.TotalPrice = sum;
        }

        public void addLocationStop(LocationStop stop)
        {
            Stops.Add(stop);
        }
    }
}