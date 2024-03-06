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
        public async void MakeJso   nAsync()
        {
            //makes json string
            string jsonString = JsonSerializer.Serialize(trip, new JsonSerializerOptions
            {
                //This will format the JSON with indentation
                WriteIndented = true
            }); ;

            //make the file name of the first hotel
            string fileName = trip.Stops.ToList().First().Hotel.Name + ".json";

            // Get dynamic file path to saves folder
            string workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"));
            string filePath = Path.Combine(workingDirectory, "Saves", fileName);

            //make file via filestream and add the text
            File.WriteAllText(filePath, jsonString);
        }

        public void MakeJson()
        {
            //makes json string
            string jsonString = JsonSerializer.Serialize(trip, new JsonSerializerOptions
            {
                //This will format the JSON with indentation
                WriteIndented = true
            }); ;

            //make the file name of the first hotel
            string fileName = trip.Stops.ToList().First().Hotel.Name + ".json";

            // Get dynamic file path to saves folder
            string workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"));
            string filePath = Path.Combine(workingDirectory, "Saves", fileName);

            //make file via filestream and add the text
            File.WriteAllTextAsync(filePath, jsonString);
        }
    }
}
