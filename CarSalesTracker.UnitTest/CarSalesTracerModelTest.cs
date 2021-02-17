using AutoFixture;

using CarSalesTracker.Model;
using CarSalesTracker.Model.Model;

using System;
using System.Data;
using System.IO;
using System.Linq;

using Xunit;

namespace CarSalesTracker.UnitTest
{
    public class CarSalesTracerModelTest
    {
        private static IFixture _fixture;

        private static IFixture Fixture
        {
            get
            {
                if (_fixture == null)
                {
                    _fixture = new Fixture().Customize(new CurrentDateTimeCustomization());

                    _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
                    _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
                }

                return _fixture;
            }
        }

        [Fact]
        public void Constructor_Constructs_WithoutParameters()
        {
            Assert.NotNull(new SalesDataMatrix());
        }

        [Fact]
        public void GetTable_ThrowsException_FileDoesntExist()
        {
            var path = Fixture.Create<string>();

            var loader = new SalesDataMatrix();

            var table = loader.GetSalesMatrix(path);

            Assert.Null(table);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetTable_ReturnsNull_FilePathNullOrEmpty(string path)
        {
            var loader = new SalesDataMatrix();

            var table = loader.GetSalesMatrix(path);

            Assert.Null(table);
        }

        [Fact]
        public void GetTable_ReturnsNull_FileExists()
        {
            var path = Path.GetTempFileName();

            var loader = new SalesDataMatrix();

            var table = loader.GetSalesMatrix(path);

            Assert.NotNull(table);
        }
        [Fact]
        public void Constructor_ThrowsArgumentNullException_NullDataTable()
        {
            DataTable table = null;

            Assert.Throws<ArgumentNullException>(() => new SalesDataMatrixProperties(table));
        }


        [Fact]
        public void ColumnNames_ReturnsColumnNames()
        {
            DataTable table = new DataTable();

            var columns = Fixture.CreateMany<string>();
            table.Columns.AddRange(columns.Select(c => new DataColumn(c)).ToArray());

            var salesTable = new SalesDataMatrixProperties(table);

            Assert.True(salesTable.Columns.SequenceEqual(columns.Skip(1)));
        }

        [Fact]
        public void Rows_ReturnsRowValues()
        {
            int colNo = Fixture.Create<int>();
            DataTable table = new DataTable();

            var columnNames = Fixture.CreateMany<string>(colNo).ToList();
            table.Columns.AddRange(columnNames.Select(s => new DataColumn(s)).ToArray());

            var columns = Fixture.CreateMany<string[]>(colNo).ToList();
            columns.ForEach(c => table.Rows.Add(c));

            var salesTable = new SalesDataMatrixProperties(table);

            Assert.True(salesTable.Rows.Zip(columns, (first, second) => first.SequenceEqual(second)).All(b => true));
        }
    }
}
