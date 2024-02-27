using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    internal class Trip
    {

        // This class is used to store information about a trip

        public double TotalPrice { get; set; }
        public HashSet<LocationStop> Stops { get; set; }


        // Constructor
        public Trip()
        {
            TotalPrice = 0;
            Stops = new HashSet<LocationStop>();
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
    }
}
