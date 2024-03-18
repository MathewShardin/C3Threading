using TripBuddy.Models;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //test code
            DataStore dataStore = new DataStore();
            dataStore.ParseFromCsv();
            List<Hotel> searchedHotels = Search.SearchHotels(dataStore.HotelCatalogue, "Le");
            var hello = "a";
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            
        }
    }
}
