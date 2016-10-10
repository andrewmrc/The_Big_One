using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

    /// <summary>
    /// Class to store one CSV row
    /// </summary>
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    /// <summary>
    /// Class to write data to a CSV file
    /// </summary>
    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename, bool append)
            : base(filename,append)
        {
        }

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(';');
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }

        public static void AddRow(string path, string[] info, bool append)
        {
            using (CsvFileWriter writer = new CsvFileWriter(path, append))
            {
                CsvRow row = new CsvRow();
                for (int j = 0; j < info.Length; j++)
                    row.Add(info[j]);
                writer.WriteRow(row);
            }
        }
        public static void AddRow(string path, string[] info)
        {
            AddRow(path, info, true);
        }
        public static void DeleteRow(string path, int id)
        {
            string[] document = CsvFileReader.ReadAll(path);    //salvo il file
            AddRow(path, document[0].Split(MyGlobal.separator), false); //cancello il file
            for(int i = 1; i < document.Length; i++)
            {
                if(i != id)
                {   //scrivo riga per riga
                    AddRow(path, document[i].Split(MyGlobal.separator));
                }
            }
        }
    }

/// <summary>
/// Class to read data from a CSV file
/// </summary>
public class CsvFileReader : StreamReader
{
    public CsvFileReader(Stream stream)
        : base(stream)
    {
    }

    public CsvFileReader(string filename)
        : base(filename)
    {
    }

    /// <summary>
    /// Reads a row of data from a CSV file
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public bool ReadRow(CsvRow row)
    {
        row.LineText = ReadLine();
        if (String.IsNullOrEmpty(row.LineText))
            return false;

        int pos = 0;
        int rows = 0;

        while (pos < row.LineText.Length)
        {
            string value;

            // Special handling for quoted field
            if (row.LineText[pos] == '"')
            {
                // Skip initial quote
                pos++;

                // Parse quoted value
                int start = pos;
                while (pos < row.LineText.Length)
                {
                    // Test for quote character
                    if (row.LineText[pos] == '"')
                    {
                        // Found one
                        pos++;

                        // If two quotes together, keep one
                        // Otherwise, indicates end of value
                        if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                        {
                            pos--;
                            break;
                        }
                    }
                    pos++;
                }
                value = row.LineText.Substring(start, pos - start);
                value = value.Replace("\"\"", "\"");
            }
            else
            {
                // Parse unquoted value
                int start = pos;
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                value = row.LineText.Substring(start, pos - start);
            }

            // Add field to list
            if (rows < row.Count)
                row[rows] = value;
            else
                row.Add(value);
            rows++;

            // Eat up to and including next comma
            while (pos < row.LineText.Length && row.LineText[pos] != ',')
                pos++;
            if (pos < row.LineText.Length)
                pos++;
        }
        // Delete any unused items
        while (row.Count > rows)
            row.RemoveAt(rows);

        // Return true if any columns read
        return (row.Count > 0);
    }


    public static string[] ReadAll(string path)
    {
        List<string> result = new List<string>();
        using (CsvFileReader reader = new CsvFileReader(path))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                result.Add(row.LineText);
            }
        }
        return result.ToArray();
    }
    public static string[] ReadSpecificRow(string path, int column, string expression)
    {
        List<string> result = new List<string>();
        using (CsvFileReader reader = new CsvFileReader(path))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                if (row.LineText.Split(MyGlobal.separator)[column] == expression)
                {
                    result.Add(row.LineText);
                }
            }
        }
        return result.ToArray();
    }
    //public static string ReadByID(string path, int id)
    //{
    //    int i = 0;
    //    string result = null;
    //    // Read sample data from CSV file
    //    using (CsvFileReader reader = new CsvFileReader(path))
    //    {
    //        CsvRow row = new CsvRow();
    //        while (reader.ReadRow(row))
    //        {
    //            if (i++ == id)
    //            {
    //                result = row.LineText;
    //                break;
    //            }
    //        }
    //    }
    //    return result;
    //}
    public static List<string> ReadOnlyColumn(string path, int column)
    {
        List<string> field = new List<string>();
        // Read sample data from CSV file
        using (CsvFileReader reader = new CsvFileReader(path))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                field.Add(row.LineText.Split(MyGlobal.separator)[column]);
            }
        }
        return field;
    }
    public static int ReadLastRow(string path, int column)
    {
        string i = "-1";
        using (CsvFileReader reader = new CsvFileReader(path))
        {
            CsvRow row = new CsvRow();
            while (reader.ReadRow(row))
            {
                i = row.LineText.Split(MyGlobal.separator)[column];
            }
        }
        return Convert.ToInt16(i);
    }
}