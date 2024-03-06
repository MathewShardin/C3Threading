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
    }

}
