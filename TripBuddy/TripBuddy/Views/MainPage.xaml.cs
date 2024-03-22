using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using TripBuddy.Models;
using TripBuddy.ViewModel;
using System.Diagnostics;

namespace TripBuddy.Views
{
    public partial class MainPage : ContentPage
    {
        DataStore dataStore = new DataStore();
        Trip tripCurrent { get; set; }


        public MainPage(MainPageViewModel vm)
        {
            // Start parsing CSV contents in seperate thread so that GUI is not frozen
            InitializeComponent();
            Thread threadCsv = new Thread(() => this.dataStore.ParseFromCsv());
            threadCsv.IsBackground = true;
            threadCsv.Start();
            // Initialize the Trip object that will contain User selection
            ResetTrip();
            threadCsv.Join(); //Wait for Threads to end and join them

            BindingContext = vm;

            // Populate the Picker with the list of cities
            //CityPicker.ItemsSource = dataStore.HotelCatalogue.Select(hotel => hotel.City.Name).Distinct().ToList(); // Assuming City has a Name property

        }

        /*private void SortHotels_Click(object sender, EventArgs e)
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
        }*/

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
        private void ResetTrip()
        {
            this.tripCurrent = new Trip();
        }
        private void SaveJsonTrip_Click(object sender, EventArgs e)
        {
            JsonSaveLoad.MakeJsonAsync(this.tripCurrent);
        }

        private void LoadJsonTrip_Click(object sender, EventArgs e)
        {
            this.tripCurrent = JsonSaveLoad.loadJson();
        }
        
        public void AddNewLocationStop(Hotel hotel)
        {
            this.tripCurrent.addLocationStop(new LocationStop(hotel));
        }

        public void AddNewLocationStop()
        {
            LocationStop tempStop = new LocationStop();
            this.tripCurrent.addLocationStop(tempStop);
        }
        public void AddNewLocationStop(LocationStop stop)
        {
            this.tripCurrent.addLocationStop(stop);
        }

        //Remove a LocationStop based on its index in the list. The order stays as users manually add new Stops top to bottom
        public void RemoveLocationStop(int index)
        {
            try
            {
                this.tripCurrent.Stops.RemoveAt(index);
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        //Remove a LocationStop based on the object itself
        public void RemoveLocationStop(LocationStop stop)
        {
            try
            {
                this.tripCurrent.Stops.Remove(stop);
            }
            catch (IndexOutOfRangeException ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        //Adds a specified Hotel object to a LocationStop with a given index (index for tripCurrent.Stops)
        public void AddHotelToLocationStop(Hotel hotel, int index)
        {
            tripCurrent.Stops[index].Hotel = hotel;
        }


    }
}