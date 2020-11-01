using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using bagel_sales_control.Core;

namespace bagel_sales_control.Features.Sales
{
    [Route("api/v1/sales")]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _saleService;

        public SalesController(SalesService salesService)
        {
            if (salesService == null) throw new ArgumentNullException(nameof(salesService));

            _saleService = salesService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetSales()
        {
            Task<Response> response = _saleService.GetSales();

            return Ok(response);
        }


        [HttpGet]
        [Route("by-productId")]
        public IActionResult GetSaleByProductId([FromQuery] SalesRequest salesRequest)
        {
            Task<Response> response = _saleService.GetSaleByProductId(salesRequest);

            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IActionResult SaveSale([FromBody] SalesRequest salesRequest)
        {
            Task<Response> response = _saleService.SaveSale(salesRequest);

            return Ok(response);
        }

        [HttpDelete]
        [Route("")]
        public IActionResult DeleteSale([FromBody] SalesRequest salesRequest)
        {
            Task<Response> response = _saleService.DeleteSale(salesRequest);

            return Ok(response);
        }

    }
}