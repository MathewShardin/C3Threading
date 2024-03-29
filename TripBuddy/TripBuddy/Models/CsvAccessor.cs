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
        const string CSVFILENAME = "HotelFinalDataset.csv";
        const string FOLDERRESOURCES = @"/Resources/Csv/";
        const string CITYCSVFILENAME = "worldcities.csv";

        //Method returns a list of arrays with string objects inside (2 dimensional array/Table) from Hotel flat file
        public static List<string[]> ReadCsvFile()
        {
            return GetCSVString(CSVFILENAME);
        }

        // Reads a CSV file from resources folder with a given fileName
        public static List<String[]> GetCSVString(String fileName)
        {
            // Get dynamic file path to a CSV file with hotel info
            string workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"));
            string filePath = workingDirectory + FOLDERRESOURCES + fileName;

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

        // Returns info about a city from a flat CSV file based on given cityName
        public static string[] GetCityInfo(string cityName)
        {
            // Check input
            if (string.IsNullOrEmpty(cityName)) { return null; }

            // Read the CSV File
            List<string[]> cityData = GetCSVString(CITYCSVFILENAME);
            // Check CSV contents
            if (cityData == null) { return null; }

            // Use PLINQ to iterate over the list in parallel and return City Info as array of strings or NULL
            var result = (from city in cityData.AsParallel() where city[0].Contains(cityName) select city).FirstOrDefault();
            return result;
        }
    }
}