using System.Data;

namespace CarSalesTracker.Model.Interfaces
{
    public interface ISalesDataMatrix
    {
        DataTable GetSalesMatrix(string path);
    }
}
