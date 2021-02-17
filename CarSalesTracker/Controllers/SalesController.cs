using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using CarSalesTracker.Model.Model;
using CarSalesTracker.Interfaces;

namespace CarSalesTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ILogger<SalesController> _logger;
        private readonly ICarSales _carSales;

        public SalesController(ILogger<SalesController> logger, ICarSales carSales)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _carSales = carSales ?? throw new ArgumentNullException(nameof(carSales));
        }

        [HttpGet]
        public async Task<SalesDataMatrixProperties> Get()
        {
            return await _carSales.GetSalesMatrix();
        }
    }
}
