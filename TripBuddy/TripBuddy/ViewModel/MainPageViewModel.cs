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
        private ObservableCollection<Hotel> _hotels;

        [ObservableProperty]
        private ObservableCollection<City> _cities;

        [ObservableProperty]
        private City _city;

        [ObservableProperty]
        private int _count;

        public MainPageViewModel()
        {
            DataStore dStore = new DataStore();

            dStore.ParseFromCsv();

            Hotels = new ObservableCollection<Hotel>(dStore.HotelCatalogue);
            Cities = new ObservableCollection<City>(dStore.CityCatalogue);
        }
    }
}
