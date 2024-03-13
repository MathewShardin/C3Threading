using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBuddy.Models
{
    static class Search
    {
        public static async Task<List<Hotel>> SearchHotelsAsync(List<Hotel> hotels, string searchQuery)
        {
            //if empty string do nothing
            if (hotels == null)
            {
                return new List<Hotel>();
            }

            //use PLINQ to search hotels in parallel
            var searchResult = await Task.Run(() =>
            {
                return hotels.AsParallel()
                    .Where(hotel => hotel.Name.ToLower().Contains(searchQuery))
                    .ToList();
            });

            return searchResult;
        }
    }
}
