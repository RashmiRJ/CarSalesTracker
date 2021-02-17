using System.Data;

namespace CarSalesTracker.Model.Interfaces
{
    public interface ISalesDataMatrixDisplay
    {
        void DisplayMatrix(DataTable dtMatrix);
    }
}
