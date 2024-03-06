using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;


namespace TripBuddy.Models
{
    internal class JsonSaveLoad
    {
        DateTime creationDate { get; }
        Trip trip = new Trip();

        public JsonSaveLoad(Trip trip)
        {
            this.trip = trip;
            creationDate = DateTime.Now;
        }

        //turns the trip and date into a Json string asynchronously
        public async void MakeJsonAsync()
        {
            //makes json string
            string jsonString = JsonSerializer.Serialize(trip);

            //make the file name of the first hotel
            string fileName = trip.Stops.ToList().First().Hotel.Name;

            //make file via filestream
            await using FileStream createStream = File.Create(fileName);

            //serialize the json into the file
            await JsonSerializer.SerializeAsync(createStream, jsonString);

        }

        public void MakeJson()
        {
            //makes json string
            string jsonString = JsonSerializer.Serialize(trip);

            //make the file name of the first hotel
            string fileName = trip.Stops.ToList().First().Hotel.Name;

            //make file via filestream
            using FileStream createStream = File.Create(fileName);

            //serialize the json into the file
            JsonSerializer.SerializeAsync(createStream, jsonString);
        }
    }
}
