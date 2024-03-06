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
            DescendingSortHotelsPrice(hotels, hotel => hotel.Price);

        }

        public void AscendingSortHotelsPrice<T>(List<T> hotels, Func<T, double> price)
        {
            //Convert the list to an array for better performance during sorting
            T[] hotelArray = hotels.ToArray();

            //Parallel quicksort
            ParallelQuickSort(hotelArray, 0, hotelArray.Length - 1, price);

            //Clear the original list and add the sorted items
            hotels.Clear();

            //Add the sorted items
            hotels.AddRange(hotelArray);
        }

        private void ParallelQuickSort<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            //Threshold for sequential quicksort
            //If the length of the partition is less than this threshold, use sequential quicksort
            const int SEQUENTIAL_THRESHOLD = 2048;

            //If the partition has more than one element
            if (right > left)
            {
                //If the partition is small enough, use sequential quicksort
                if (right - left < SEQUENTIAL_THRESHOLD)
                {
                    // Sequential quicksort for small partitions
                    QuickSortSequential(arr, left, right, price);
                }
                else
                {
                    // Parallel quicksort for larger partitions
                    int pivot = Partition(arr, left, right, price);
                    //Invoke two tasks to sort the two partitions in parallel
                    Parallel.Invoke(
                        //Sort the left partition
                        () => ParallelQuickSort(arr, left, pivot - 1, price),
                        //Sort the right partition
                        () => ParallelQuickSort(arr, pivot + 1, right, price)
                    );
                }
            }
        }
        private void QuickSortSequential<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            //If the left index is less than the right index
            if (left < right)
            {
                //Partition the array and get the pivot index
                int pivotIndex = Partition(arr, left, right, price);
                //Recursively sort the left partition
                QuickSortSequential(arr, left, pivotIndex - 1, price);
                //Recursively sort the right partition
                QuickSortSequential(arr, pivotIndex + 1, right, price);
            }
        }
        private int Partition<T>(T[] arr, int left, int right, Func<T, double> price)
        {
            //Choose the rightmost element as the pivot
            T pivot = arr[right];
            //The index of the smaller element
            int i = left - 1;


            for (int j = left; j < right; j++)
            {
                //If the current element is smaller than or equal to the pivot
                if (price(arr[j]) <= price(pivot))
                {

                    i++;
                    Swap(arr, i, j);
                }
            }
            //Move the pivot to its correct position
            Swap(arr, i + 1, right);
            return i + 1;
        }

        private void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public void DescendingSortHotelsPrice<T>(List<T> hotels, Func<T, double> price)
        {
            //Call the AscendingSortHotelsPrice method
            AscendingSortHotelsPrice(hotels, price);

            //Reverse the list to get descending order
            hotels.Reverse();
        }

    }
}

