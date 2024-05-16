using System;
using System.IO;
using System.Linq;

namespace DSA
{
    internal static class CSVManager
    {
        public static string[] ParseFile(string FilePath) //returns all the rows of the csv file
        {
            if (Path.GetExtension(FilePath) != ".csv")
            {
                throw new FormatException("Error: file specified is not a CSV");
            }
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("Error: no such file exists.");
            }

            //at this point, file exists and is a csv file
            string[] FullData;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                try
                {
                    FullData = reader.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch
                {
                    return null;
                }
            }
            return FullData; //returns all not-null data rows
        }

        public static double[][] ParseFileDouble(string FilePath)
        {
            if (Path.GetExtension(FilePath) != ".csv")
            {
                throw new FormatException("Error: file specified is not a CSV");
            }
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("Error: no such file exists.");
            }

            // At this point, file exists and is a csv file
            string[] FullData;
            double[][] ParsedData;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                try
                {
                    FullData = reader.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch
                {
                    return null;
                }
            }

            ParsedData = new double[FullData.Length][];
            for (int i = 0; i < FullData.Length; i++)
            {
                string[] values = FullData[i].Split(',');

                ParsedData[i] = values.Select(s =>
                {
                    if (double.TryParse(s, out double result))
                        return result;
                    else
                        throw new FormatException($"Unable to parse '{s}' to double.");
                }).ToArray();
            }
            return ParsedData; // returns all not-null data rows
        }



        public static void SaveToCSV(string FilePath, string[] CSVData, bool overwrite = false)
        {
            string FileName = Path.GetFileNameWithoutExtension(FilePath);
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            while (File.Exists(FilePath) && !overwrite)
            {
                FileName += " - Copy";
                FilePath = Path.GetDirectoryName(FilePath) + FileName + ".csv"; //creates a copy of the file
            }
            using StreamWriter writer = new StreamWriter(FilePath, false);
            foreach (string line in CSVData)
            {
                writer.WriteLine(line);
            }
        }
    }
}
