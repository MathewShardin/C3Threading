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

            // Populate the Picker with the list of cities
            //CityPicker.ItemsSource = dataStore.HotelCatalogue.Select(hotel => hotel.City.Name).Distinct().ToList(); // Assuming City has a Name property

        }

        private void SortHotels_Click(object sender, EventArgs e)
        {
            {
                if (CityPicker.SelectedItem != null)
                {
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        // Get the selected city
                        var selectedCity = CityPicker.SelectedItem.ToString();

                        // Filter and sort the hotels by price in ascending order
                        var sortedHotels = dataStore.HotelCatalogue
                            .Where(hotel => hotel.City.Name == selectedCity) // Assuming City has a Name property
                            .OrderBy(hotel => hotel.Price)
                            .ToList();

                        //sortedHotels = sortedHotels.Take(10).ToList();

                        // Create entries for the chart based on sorted hotel prices
                        var entries = sortedHotels.Select(hotel =>
                                        new Microcharts.ChartEntry((float)hotel.Price)
                                        {
                                            Label = hotel.Name,
                                            ValueLabel = hotel.Price.ToString(),
                                            Color = SKColor.Parse("#266489")
                                        }).ToList();

                        var lineChart = new LineChart()
                        {
                            Entries = entries,
                            LabelTextSize = 10f, // Adjust the text size
                            ValueLabelOrientation = Orientation.Horizontal, // Change the orientation
                            LabelOrientation = Orientation.Horizontal, // Change the orientation
                        };

                        // Update the UI on the main thread
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            chartView.Chart = lineChart;
                        });
                    });
                }
            }
        }
    }
}