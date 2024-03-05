using TripBuddy.Models;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var file = CsvAccessor.ReadCsvFile();
            CounterBtn.Text = $"text changed";

            //Making trip
            Trip trip = new Trip();
            //Making city
            City city = new City("towscity");
            //Making Hotel
            Hotel hotel = new Hotel("hotelplace", 1000, city, "nice place", 1);
            //Making Location Stop
            LocationStop stop = new LocationStop(hotel, DateTime.Now);
            //add stop to trip
            trip.AddStop(stop);

            JsonSaveLoad json = new JsonSaveLoad(trip);

            await json.MakeJsonAsync();

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
