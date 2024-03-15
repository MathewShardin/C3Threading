using Microcharts.Maui;
using Microcharts;
using SkiaSharp;
using TripBuddy.Models;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void SortHotels_Click(object sender, EventArgs e)
        {
            // Create an instance of DataStore
            DataStore dataStore = new DataStore();

            // Parse the hotels from the CSV file
            dataStore.ParseFromCsv();

            // Sort the hotels by price in ascending order
            var sortedHotels = dataStore.HotelCatalogue.OrderBy(hotel => hotel.Price).ToList();

            // Create entries for the chart based on sorted hotel prices
            var entries = sortedHotels.Select(hotel =>
                            new Microcharts.ChartEntry((float)hotel.Price)
                            {
                                Label = hotel.Name,
                                ValueLabel = hotel.Price.ToString(),
                                Color = SKColor.Parse("#266489")
                            }).ToList();

            // Update the chart view with the entries
            chartView.Chart = new BarChart() { Entries = entries };
        }




        public void AscendingSortHotelsPrice<T>(List<T> hotels, Func<T, IComparable> keySelector)
        {
            //Convert the list to an array for better performance
            T[] hotelArray = hotels.ToArray();

            //Perform parallel quicksort
            ParallelQuickSort(hotelArray, 0, hotelArray.Length - 1, keySelector);

            //Clear the original list and add the sorted items
            hotels.Clear();
            hotels.AddRange(hotelArray);
        }

        private void ParallelQuickSort<T>(T[] arr, int left, int right, Func<T, IComparable> keySelector)
        {
            // Threshold for sequential quicksort
            const int SEQUENTIAL_THRESHOLD = 2048;

            if (right > left)
            {
                // If the partition is small enough, use sequential quicksort
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    // Sequential quicksort for small partitions
                    QuickSortSequential(arr, left, right, keySelector);
                }
                else
                {
                    // Parallel quicksort for larger partitions
                    int pivot = Partition(arr, left, right, keySelector);
                    // Invoke two tasks to sort the two partitions in parallel
                    Parallel.Invoke(
                        // Sort the left partition
                        () => ParallelQuickSort(arr, left, pivot - 1, keySelector),
                        // Sort the right partition
                        () => ParallelQuickSort(arr, pivot + 1, right, keySelector)
                    );
                }
            }
        }
        private void QuickSortSequential<T>(T[] arr, int left, int right, Func<T, IComparable> keySelector)
        {
            // If the left index is less than the right index
            if (left < right)
            {
                // Partition the array and get the pivot index
                int pivotIndex = Partition(arr, left, right, keySelector);
                // Recursively sort the left partition
                QuickSortSequential(arr, left, pivotIndex - 1, keySelector);
                // Recursively sort the right partition
                QuickSortSequential(arr, pivotIndex + 1, right, keySelector);
            }
        }
        private int Partition<T>(T[] arr, int left, int right, Func<T, IComparable> keySelector)
        {
            // Choose the rightmost element as the pivot
            T pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (keySelector(arr[j]).CompareTo(keySelector(pivot)) <= 0)
                {
                    i++;
                    // Swap arr[i] and arr[j]
                    T temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // Swap arr[i+1] and arr[right] (pivot)
            T tempPivot = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = tempPivot;

            return i + 1;
        }

        public void DescendingSortHotelsPrice<T>(List<T> hotels, Func<T, IComparable> keySelector)
        {
            //call the AscendingSortHotelsPrice method
            AscendingSortHotelsPrice(hotels, keySelector);

            //reverse the list to get descending order
            hotels.Reverse();
        }

        public void AscendingSortHotelNames<T>(List<T> hotels, Func<T, IComparable> keySelector)
        {
            //Convert the list to an array for better performance
            T[] hotelArray = hotels.ToArray();

            //Perform parallel quicksort
            ParallelQuickSort(hotelArray, 0, hotelArray.Length - 1, keySelector);

            //Clear the original list and add the sorted items
            hotels.Clear();
            hotels.AddRange(hotelArray);
        }

    }
}

