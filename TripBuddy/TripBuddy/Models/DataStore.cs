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
            // read the csv file
            List<string[]> csventries = CsvAccessor.ReadCsvFile();
            // parse the data into custom objects type hotel
            List<Hotel> hotels = csventries.AsParallel().Skip(1)
                       .Select(data => new Hotel(data[1], Convert.ToDouble(data[4]), GetCityByName(data[7]), data[3], Convert.ToInt32(data[0])))
                       .ToList();
            // save the list of ojects into the field
            HotelCatalogue = hotels;
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
                if (price(arr[j]).CompareTo(price(pivot)) <= 0) //use compareto for comparison
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

        public void DescendingSortHotelsPrice<T>(List<T> hotels, Func<T, double> price)
        {
            //call the AscendingSortHotelsPrice method
            AscendingSortHotelsPrice(hotels, price);

            //reverse the list to get descending order
            hotels.Reverse();
        }
    }
}
