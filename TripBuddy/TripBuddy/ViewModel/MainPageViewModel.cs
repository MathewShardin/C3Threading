using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBuddy.Models;

namespace TripBuddy.ViewModel
{
    internal class MainPageViewModel
    {
        public int Count { get; private set; }

        public MainPageViewModel()
        {
            Count = 0;
            CsvAccessor.ReadCsvFile();
        }
    }
}
