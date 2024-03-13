using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBuddy.Models;

namespace TripBuddy.ViewModel
{
    public partial class MainPageViewModel
    {
        public int Count { get; private set; }
        
        private Hotel _hotel;


        public City City { get; set; }

        public Hotel Hotel
        {
            get => _hotel;
            set
            {
                _hotel = value;
            }
        }   



        public MainPageViewModel()
        {
            Count = 0;
            City = new City("City", "Country","010002000" );
            Hotel = new Hotel("Hotel", 100,City,"good",1);
            //CsvAccessor.ReadCsvFile();

        }

    }
}
