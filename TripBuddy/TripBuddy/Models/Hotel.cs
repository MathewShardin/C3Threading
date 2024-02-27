using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    internal class Hotel
    {
        // This is a custom data type to store information about a hotel

        public string Name { get; set; }

        public double Price { get; set; }

        public City City { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }


        // Constructor
        public Hotel(string name, double price, City city, string description, int id)
        {
            Name = name;
            Price = price;
            City = city;
            Description = description;
            Id = id;
        }
    }
}
