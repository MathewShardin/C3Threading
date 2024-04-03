using TripBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace TripBuddy.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Hotel _hotel;

        [ObservableProperty]
        public ObservableCollection<Hotel> _hotels;

        [ObservableProperty]
        private ObservableCollection<City> _cities;

        [ObservableProperty]
        private City _city;

        [ObservableProperty]
        private int _count;

        [ObservableProperty]
        private double _price;

        public MainPageViewModel()
        {
            DataStore dStore = new DataStore();
            Trip trip = new Trip();

            dStore.ParseFromCsv();

            Hotels = new ObservableCollection<Hotel>(dStore.HotelCatalogue);
            Cities = new ObservableCollection<City>(dStore.CityCatalogue);
            Price = trip.TotalPrice;
        }

        public ObservableCollection<Hotel> getHotels()
        {
            return Hotels;
        }

        public bool setHotels(ObservableCollection<Hotel> hotels)
        {
            if (hotels != null)
            {
                Hotels = hotels;
                return true;
            } else
            {
                return false;
            }
        }
    }
}
