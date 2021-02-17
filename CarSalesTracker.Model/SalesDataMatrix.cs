using CsvReader;

using System.Data;
using System.IO;

using CarSalesTracker.Model.Interfaces;

namespace CarSalesTracker.Model
{
    public class SalesDataMatrix : ISalesDataMatrix
    {
        //Load csv Data
        //Get column names and rows as the first of each from the CSV
        //If no issues, load the matrix
        public DataTable GetSalesMatrix(string path)
        {
            DataTable dt = null;

            try
            {
                using (var csv = new CachedCsvReader(new StreamReader(path), true))
                {
                    dt = new DataTable();
                    dt.Load(csv);
                }
            }
            catch { }

            return dt;
        }
    }
}
