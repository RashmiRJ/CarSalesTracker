using System.Collections.Generic;

namespace CarSalesTracker.Model.Interfaces
{
    public interface ISalesDataMatrixProperties
    {
        IEnumerable<string> Columns { get; }

        IEnumerable<string[]> Rows { get; }

    }
}
