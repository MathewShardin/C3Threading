﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.Maui.Storage;
using System.Diagnostics;

namespace TripBuddy.Models
{
    internal static class CsvAccessor
    {
        const string CSVFILENAME = "hotelbookingdata.csv";
        const string FOLDERRESOURCES = @"/Resources/Csv/";

        //Method returns a list of arrays with string objects inside (2 dimensional array/Table) 
        public static List<string[]> ReadCsvFile()
        {
            // Get dynamic file path to a CSV file with hotel info
            string workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"));
            string filePath = workingDirectory + FOLDERRESOURCES + CSVFILENAME;

            //Checks if the string is not null
            if (string.IsNullOrEmpty(filePath)) { return null; }

            try
            {
                List<string[]> data = new List<string[]>();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        data.Add(line.Split(','));
                    }
                }
                //Also contains header!!!
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV data: {ex.Message}");
                return null;
            }
        }
    }
}