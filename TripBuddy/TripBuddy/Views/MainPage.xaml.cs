using TripBuddy.Models;

namespace TripBuddy
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            var file = CsvAccessor.ReadCsvFile();
            CounterBtn.Text = $"text changed";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void SortHotels_Clicked(object sender, EventArgs e)
        {
            // create a list of hotels
            List<Hotel> hotels = new List<Hotel>
            {
                new Hotel("Hotel C", 150.0, new City("City 1"), "Description C", 3),
                new Hotel("Hotel A", 100.0, new City("City 1"), "Description A", 1),
                new Hotel("Hotel B", 120.0, new City("City 2"), "Description B", 2),
                new Hotel("Hotel D", 200.0, new City("City 3"), "Description D", 4)
            };

            // sort the hotels
            AscendingSortHotelsPrice(hotels, hotel => hotel.Price);

        }

        public void AscendingSortHotelsPrice<T>(List<T> hotels, Func<T, double> price)
        {
            // Convert the list to an array for better performance
            T[] hotelArray = hotels.ToArray();

            // Perform parallel quicksort
            ParallelQuickSort(hotelArray, 0, hotelArray.Length - 1, price);

            // Clear the original list and add the sorted items
            hotels.Clear();
            hotels.AddRange(hotelArray);
        }

        private void ParallelQuickSort<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            const int SEQUENTIAL_THRESHOLD = 2048;

            if (right > left)
            {
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    // Sequential quicksort for small partitions
                    QuickSortSequential(arr, left, right, price);
                }
                else
                {
                    // Parallel quicksort for larger partitions
                    int pivot = Partition(arr, left, right, price);
                    Parallel.Invoke(
                        () => ParallelQuickSort(arr, left, pivot - 1, price),
                        () => ParallelQuickSort(arr, pivot + 1, right, price)
                    );
                }
            }
        }
        private void QuickSortSequential<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right, price);
                QuickSortSequential(arr, left, pivotIndex - 1, price);
                QuickSortSequential(arr, pivotIndex + 1, right, price);
            }
        }
        private int Partition<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            T pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (price(arr[j]) <= price(pivot))
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}

