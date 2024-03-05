using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


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

        public async void makeJSON()
        {
                        
        }
    }
}
