using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    public class Hotel : IComparable
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

        public int CompareTo(object? obj)
        {
            if (obj is null)
            {
                throw new NoNullAllowedException();
            }

            if (this.GetType() != obj.GetType())
            {
                throw new ArgumentException("Object is not a Hotel");
            }

            Hotel otherHotel = (Hotel)obj;

            if (this.Price is double && otherHotel.Price is double)
            {
                if (this.Price < otherHotel.Price)
                {
                    return -1;
                }
                else if (this.Price > otherHotel.Price)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                throw new ArgumentException("Object is not a number");
            }
        }
    }
}
