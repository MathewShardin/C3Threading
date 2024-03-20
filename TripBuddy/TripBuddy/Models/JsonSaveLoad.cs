using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace TripBuddy.Models
{
    internal class JsonSaveLoad
    {
        //DateTime creationDate { get; }
        //Trip trip = new Trip();

        public JsonSaveLoad()
        {
        }

        //turns the trip and date into a Json string asynchronously
        public static async void MakeJsonAsync(Trip trip)
        {
            //makes json string
            string jsonString = System.Text.Json.JsonSerializer.Serialize(trip, new JsonSerializerOptions
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

        public void MakeJson(Trip trip)
        {
            //makes json string
            string jsonString = System.Text.Json.JsonSerializer.Serialize(trip, new JsonSerializerOptions
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

        public static Trip loadJson()
        {
            //get file
            string fileName = "hotelplace.json";
            //Get dynamic file path to saves folder
            string workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"));
            string filePath = Path.Combine(workingDirectory, "Saves", fileName);

            //read file
            var jsonFile = JObject.Parse(File.ReadAllText(filePath));
            string jsonString = jsonFile.ToString();
            Trip trip = JsonConvert.DeserializeObject<Trip>(jsonString);
            return trip;
        }
    }
}
