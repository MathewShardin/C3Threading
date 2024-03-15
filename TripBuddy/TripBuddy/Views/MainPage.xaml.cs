using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using TripBuddy.Models;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        DataStore dataStore = new DataStore();


        public MainPage()
        {
            InitializeComponent();
            dataStore.ParseFromCsv();

        }

        private void SortHotels_Click(object sender, EventArgs e)
        {

              // Sort the hotels by price in ascending order
            var sortedHotels = dataStore.HotelCatalogue.OrderBy(hotel => hotel.Price).ToList();

            sortedHotels = sortedHotels.Take(10).ToList();

            // Create entries for the chart based on sorted hotel prices
            var entries = sortedHotels.Select(hotel =>
                            new Microcharts.ChartEntry((float)hotel.Price)
                            {
                                Label = hotel.Name,
                                ValueLabel = hotel.Price.ToString(),
                                Color = SKColor.Parse("#266489")
                            }).ToList();

            var barChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 20f, // Adjust the text size
                ValueLabelOrientation = Orientation.Horizontal, // Change the orientation
                LabelOrientation = Orientation.Horizontal, // Change the orientation
            };

            chartView.Chart = barChart;
        }
    }
}

