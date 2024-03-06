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
            // Create a local copy of the list
            var localItems = new List<T>(hotels);

            // Using Parallel.ForEach to sort the items in parallel
            Parallel.ForEach(localItems, (item, state) =>
            {
                bool swapped;
                do
                {
                    swapped = false;
                    for (int i = 0; i < localItems.Count - 1; i++)
                    {
                        for (int j = 0; j < localItems.Count - i - 1; j++)
                        {
                            // If the price of the current item is greater than the next item
                            if (price(localItems[j]).CompareTo(price(localItems[j + 1])) > 0)
                            {
                                // Swapping the elements
                                var temp = localItems[j];
                                localItems[j] = localItems[j + 1];
                                localItems[j + 1] = temp;
                                swapped = true;
                            }
                        }
                    }
                } while (swapped);

                // If the local list is sorted, add it to the original list
                if (!swapped)
                {
                    lock (hotels)
                    {
                        hotels.Clear();
                        hotels.AddRange(localItems);
                    }

                    // Stop the parallel execution
                    state.Stop();
                }
            });
        }
    }

}
