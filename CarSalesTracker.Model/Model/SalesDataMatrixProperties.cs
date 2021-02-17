using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using CarSalesTracker.Model.Interfaces;

namespace CarSalesTracker.Model.Model
{
    public class SalesDataMatrixProperties : ISalesDataMatrixProperties
    {
        private const int emptyCols = 1;

        private readonly DataTable _table;

        /// <summary>
        /// Constructs instance of the <see cref="SalesTableData"/> class
        /// </summary>
        /// <param name="table">Data table of sales data</param>
        public SalesDataMatrixProperties(DataTable table)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }

        /// <summary>
        /// Gets column names
        /// </summary>
        public IEnumerable<string> Columns => _table.Columns.Cast<DataColumn>().Skip(emptyCols).Select(c => c.ColumnName);

        /// <summary>
        /// Gets rows
        /// </summary>
        public IEnumerable<string[]> Rows => _table.Rows.Cast<DataRow>().Select(c => c.ItemArray.Select(o => o.ToString()).ToArray());
    }
}
