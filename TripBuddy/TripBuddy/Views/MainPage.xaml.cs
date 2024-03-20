﻿using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using TripBuddy.Models;
using System.Diagnostics;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        DataStore dataStore = new DataStore();


        public MainPage()
        {
            // Initialize the GUI and start parsing CSV contents in seperate thread so that GUI is not frozen
            InitializeComponent();
            Thread threadCsv = new Thread(() => this.dataStore.ParseFromCsv());
            threadCsv.IsBackground = true;
            //Thread thread_gui_start = new Thread(InitializeComponent);
            //thread_gui_start.Start();
            threadCsv.Start();
            //Wait for Threads to end and join them
            //thread_gui_start.Join();
            threadCsv.Join();

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
    }
}