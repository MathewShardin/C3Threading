using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    public static class Search
    {
        //takes a list of hotels and search for a specific string given and return all hotel objects containing the string somewhere within their name
        public static List<Hotel> SearchHotels(List<Hotel> hotels, string searchString)
        {
            //use PLINQ to search all hotel names if they contain the searchString
            var results = hotels.AsParallel()
                                .Where(hotel => hotel.Name.Contains(searchString))
                                .ToList();

            return results;
        }
    }

}
