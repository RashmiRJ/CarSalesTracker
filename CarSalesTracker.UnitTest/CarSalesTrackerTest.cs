using AutoFixture;

using CarSalesTracker.Model.Interfaces;
using CarSalesTracker.Model.Model;
using CarSalesTracker.Interfaces;
using CarSalesTracker.Controllers;

using NSubstitute;

using System;
using System.Data;
using System.Threading.Tasks;

using Xunit;

using Microsoft.Extensions.Logging;

namespace CarSalesTracker.UnitTest
{
    public class CarSalesTrackerTest
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
        public void Constructor_ThrowsArgumentNullException_NullTableLoader()
        {
            ISalesDataMatrix loader = null;
            var path = Fixture.Create<string>();

            Assert.Throws<ArgumentNullException>(() => new CarSales(loader, path));
        }


        [Fact]
        public void GetSalesMatrix_ReturnsMatrix()
        {
            var path = Fixture.Create<string>();
            var table = new DataTable();
            var loader = Substitute.For<ISalesDataMatrix>();
            loader.GetSalesMatrix(path).Returns(c => table);

            var sales = new CarSales(loader, path);

            var result = sales.GetSalesMatrix().GetAwaiter().GetResult();

            Assert.NotNull(result);
        }


        [Fact]
        public void Constructor_ThrowsArgumentNullException_Nullsales()
        {
            var logger = Substitute.For<ILogger<SalesController>>();
            ICarSales salesTableMatrix = null;

            Assert.Throws<ArgumentNullException>(() => new SalesController(logger, salesTableMatrix));
        }

        [Fact]
        public void Constructor_Constructs_ValidArguments()
        {
            var logger = Substitute.For<ILogger<SalesController>>();
            var salesTableMatrix = Substitute.For<ICarSales>();

            var controller = new SalesController(logger, salesTableMatrix);
            Assert.NotNull(controller);
        }

        [Fact]
        public void Get_ReturnsSameSalesException()
        {
            var logger = Substitute.For<ILogger<SalesController>>();

            var exc = Fixture.Create<Exception>();
            var salesTableMatrix = Substitute.For<ICarSales>();
            salesTableMatrix.GetSalesMatrix().Returns<Task<SalesDataMatrixProperties>>(c => throw exc);

            var controller = new SalesController(logger, salesTableMatrix);

            var exception = Assert.ThrowsAny<Exception>(() => controller.Get().GetAwaiter().GetResult());
            Assert.Same(exc, exception);
        }
    }
}
