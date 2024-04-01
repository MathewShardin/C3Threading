using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    public class LocationStop
    {
        // This class defines a place where a user wants to stay

        public Hotel? Hotel { get; set; }

        public DateTime date { get; set; }

        // Constructor

        public LocationStop(Hotel hotel, DateTime date)
        {
            Hotel = hotel;
            this.date = date;
        }

        public LocationStop(Hotel hotel)
        {
            Hotel = hotel;
            this.date = DateTime.Now;
        }

        public LocationStop()
        {
            this.date = DateTime.Now;
            Hotel = null;

        }

    }
}