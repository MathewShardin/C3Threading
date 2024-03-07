namespace TripBuddy.Models
{
    internal class DataStore
    {
        private object Assert;

        // This class contains hotel information from the csv file

        public List<Hotel> HotelCatalogue { get; set; }
        public List<City> CityCatalogue { get; set; }


        // Constructor
        public DataStore()
        {
            HotelCatalogue = new List<Hotel>();
            CityCatalogue = new List<City>();
        }

        public void AddHotel(Hotel hotel)
        {
            HotelCatalogue.Add(hotel);
        }
        public void AddCity(City cityInp)
        {
            CityCatalogue.Add(cityInp);
        }
        public City GetCityByName(string cityName)
        {
            //TODO MAKE THIS MORE EFFICIENT
            // Find the city in the list
            foreach (City entry in CityCatalogue)
            {
                if (entry.Name == cityName) { return entry; }
            }

            // If it doesn't exist make a new City object and save it
            City cityTemp = new City(cityName);
            AddCity(cityTemp);
            // Return last inserted element
            return CityCatalogue.Last();
        }

        public void ParseFromCsv()
        {
            //read the csv file
            List<string[]> csventries = CsvAccessor.ReadCsvFile();
            //parse the data into custom objects type hotel
            List<Hotel> hotels = csventries.AsParallel().Skip(1)
                       .Select(data => new Hotel(data[1], Convert.ToDouble(data[4]), GetCityByName(data[7]), data[3], Convert.ToInt32(data[0])))
                       .ToList();
            // save the list of ojects into the field
            HotelCatalogue = hotels;
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

        public void DescendingSortHotelNames<T>(List<T> hotels, Func<T, IComparable> keySelector)
        {
            //call the AscendingSortHotelNames method
            AscendingSortHotelNames(hotels, keySelector);

            //reverse the list to get descending order
            hotels.Reverse();
        }
    }
}
