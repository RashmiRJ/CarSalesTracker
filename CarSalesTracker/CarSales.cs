using System;
using System.Threading.Tasks;

using CarSalesTracker.Interfaces;

using CarSalesTracker.Model.Interfaces;
using CarSalesTracker.Model.Model;

namespace CarSalesTracker
{
    public class CarSales : ICarSales
    {
        private readonly string _filepath;
        private readonly ISalesDataMatrix _matrix;

        // Constructs and instance of the <see cref="SalesTableFactory"/> class
        public CarSales(ISalesDataMatrix tableLoader, string filepath)
        {
            _filepath = !string.IsNullOrWhiteSpace(filepath) ? filepath : throw new ArgumentNullException(nameof(filepath));
            _matrix = tableLoader ?? throw new ArgumentNullException(nameof(tableLoader));
        }

        // Gets table
        public async Task<SalesDataMatrixProperties> GetSalesMatrix()
        {
            var table = await Task.Factory.StartNew(() => _matrix.GetSalesMatrix(_filepath));

            return new SalesDataMatrixProperties(table);
        }
    }
}
