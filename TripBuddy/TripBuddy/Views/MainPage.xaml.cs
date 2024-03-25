using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using TripBuddy.Models;
using TripBuddy.ViewModel;
using System.Diagnostics;
using System.Collections.ObjectModel;


namespace TripBuddy.Views
{
    public partial class MainPage : ContentPage
    {
        DataStore dataStore = new DataStore();



        public MainPage(MainPageViewModel vm)
        {
            // Initialize the GUI and start parsing CSV contents in seperate thread so that GUI is not frozen
            InitializeComponent();
            BindingContext = vm;
            Thread threadCsv = new Thread(() => this.dataStore.ParseFromCsv());
            threadCsv.IsBackground = true;
            //Thread thread_gui_start = new Thread(InitializeComponent);
            //thread_gui_start.Start();
            threadCsv.Start();
            //Wait for Threads to end and join them
            //thread_gui_start.Join();
            threadCsv.Join();    
           
            
                

        }

        private void OnClickNewPicker(object sender, EventArgs e)
        {
            // Create a new picker
            Picker newPicker = new Picker
            {
                Title = "Choose A Start",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                ItemsSource = { "Cities" }, // Set the items source
                ItemDisplayBinding = new Binding("Name") // Set the item display binding
            };

            // Add a selected index changed event handler for the new picker
            newPicker.SelectedIndexChanged += SortHotels_Click;

            // Add the new picker to the layout
            CitiesContainer.Children.Add(newPicker);
        }

        private void SortHotels_Click(object sender, EventArgs e)
        {
            // Determine which picker triggered the event
            Picker picker = sender as Picker;

            if (picker.SelectedItem != null)
            {
                // Get the selected city
                var selectedCity = (picker.SelectedItem as City).Name; // Change this line

                ThreadPool.QueueUserWorkItem(o =>
                {
                    // Filter and sort the hotels by price in ascending order
                    var sortedHotels = dataStore.HotelCatalogue
                        .Where(hotel => hotel.City.Name == selectedCity)
                        .OrderBy(hotel => hotel.Price)
                        .ToList();

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

            private async void SortByPriceAscending_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataStore.AscendingSortHotelsPrice(dataStore.HotelCatalogue, hotel => hotel.Price));
        }

        private async void SortByPriceDescending_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataStore.DescendingSortHotelsPrice(dataStore.HotelCatalogue, hotel => hotel.Price));
        }

        private async void SortByHotelNamesAscending_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataStore.AscendingSortHotelNames(dataStore.HotelCatalogue, hotel => hotel.Name));
        }

        private async void SortByHotelNamesDescending_Click(object sender, EventArgs e)
        {
            await Task.Run(() => dataStore.DescendingSortHotelNames(dataStore.HotelCatalogue, hotel => hotel.Name));
        }

        private void Onclicked(object sender, EventArgs e)
        {
            if(sender is Label label)
            {
                DisplayAlert("Label Clicked", label.Text, "OK");
            }
        }
        
        
        
    }
}