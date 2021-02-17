using System.Threading.Tasks;

using CarSalesTracker.Model.Model;

namespace CarSalesTracker.Interfaces
{
    public interface ICarSales
    {
        Task<SalesDataMatrixProperties> GetSalesMatrix();
    }
}
