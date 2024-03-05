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
        string creationDate { get; }
        Trip trip = new Trip();

        public JsonSaveLoad(Trip trip)
        {
            this.trip = trip;
        }

        public async void MakeJsonAsync()
        {
            string jsonString = JsonSerializer.Serialize(trip);
            Debug.WriteLine(jsonString);
        }

        public void MakeJson()
        {
            string jsonString = JsonSerializer.Serialize(trip);
            Debug.WriteLine(jsonString);
        }
    }
}
