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
        private ObservableCollection<Hotel> _hotels = new();

        [ObservableProperty]
        private City _city;

        [ObservableProperty]
        private int _count;

        public MainPageViewModel()
        {
            this.Count = 0;
            this.City = new City("City", "Country","010002000");
            this.Hotel = new Hotel("Hotel", 100, City, "good", 1);
            this.Hotels.Add(this.Hotel);
        }
    }
}
