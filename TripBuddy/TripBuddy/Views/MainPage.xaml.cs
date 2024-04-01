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
        List<Hotel> hotelListSave = new List<Hotel>(); // List utilized by Search Function
        MainPageViewModel viewModel;
        Trip tripCurrent { get; set; } // Contains Current User Selection
        int numOfPickers { get; set; }


        public MainPage(MainPageViewModel vm)
        {
            // Initialize the GUI and start parsing CSV contents in seperate thread so that GUI is not frozen
            InitializeComponent();
            BindingContext = vm;
            Thread threadCsv = new Thread(() => this.dataStore.ParseFromCsv());
            threadCsv.IsBackground = true;
            threadCsv.Start();
            // Initialize the Trip object that will contain User selection
            ResetTrip();
            threadCsv.Join(); //Wait for Threads to end and join them
            viewModel = vm;
        }

        private void OnClickNewPicker(object sender, EventArgs e)
        {
            // Increment the numOfPickers
            numOfPickers++;

            // Create a horizontal stack similar to that of how it is in our maui
            var newHorizontalStackLayout = new HorizontalStackLayout
            {
                Padding = new Thickness(15),
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            // Make the picker
            Picker newPicker = new Picker
            {
                Title = $"Stop number {numOfPickers}",
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            // Define the bindings
            newPicker.SetBinding(Picker.ItemsSourceProperty, "Cities");
            newPicker.ItemDisplayBinding = new Binding("Name");

            // Add a selected index changed event handler for the new picker
            newPicker.SelectedIndexChanged += SortHotels_Click;

            // Add the picker to the horizontal layout
            newHorizontalStackLayout.Children.Add(newPicker);

            // Add the stacklayout to the city container vertical layout
            CitiesContainer.Children.Add(newHorizontalStackLayout);

            // Update the layout
            this.ForceLayout();
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

            //change the viewed list on the right to only hotels within selected city
            viewModel.setHotels(new ObservableCollection<Hotel>(dataStore.HotelCatalogue));
            hotelListSave = Search.SearchHotelsWithCity(viewModel.getHotels().ToList(), (City)picker.SelectedItem);
            viewModel.setHotels(new ObservableCollection<Hotel>(hotelListSave));
        }

        private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            //get the current piece of text in bar
            string text = e.NewTextValue.ToString();

            //get the searched hotel list
            var hotelList = Search.SearchHotels(hotelListSave, text);
            viewModel.setHotels(new ObservableCollection<Hotel>(hotelList));
        }

        private async void SortByPriceAscending_Click(object sender, EventArgs e)
        {
            List<Hotel> sortedHotels = await Task.Run(() => dataStore.AscendingSortHotelsPrice(viewModel.getHotels().ToList(), hotel => hotel.Price));

            viewModel.setHotels(new ObservableCollection<Hotel>(sortedHotels));

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
        }

        private async void SortByPriceDescending_Click(object sender, EventArgs e)
        {
            List<Hotel> sortedHotels = await Task.Run(() => dataStore.DescendingSortHotelsPrice(viewModel.getHotels().ToList(), hotel => hotel.Price));

            viewModel.setHotels(new ObservableCollection<Hotel>(sortedHotels));

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
        }

        private async void SortByHotelNamesAscending_Click(object sender, EventArgs e)
        {
            List<Hotel> sortedHotels = await Task.Run(() => dataStore.AscendingSortHotelNames(viewModel.getHotels().ToList(), hotel => hotel.Name));

            viewModel.setHotels(new ObservableCollection<Hotel>(sortedHotels));

            // Create entries for the chart based on sorted hotel names
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
        }


        private async void SortByHotelNamesDescending_Click(object sender, EventArgs e)
        {
            List<Hotel> sortedHotels = await Task.Run(() => dataStore.DescendingSortHotelNames(viewModel.getHotels().ToList(), hotel => hotel.Name));

            viewModel.setHotels(new ObservableCollection<Hotel>(sortedHotels));

            // Create entries for the chart based on sorted hotel names
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
        }


        private void Onclicked(object sender, EventArgs e)
        {
            // Get Hotel object from the label clicked by user
            var labelTemp = (Label)sender;
            var hotel = (Hotel)labelTemp.BindingContext;

            if (hotel is Hotel hotelTemp)
            {
                AddNewLocationStop(hotelTemp);
                DisplayAlert("Hotel Saved!", hotelTemp.Name, "OK");
            }
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