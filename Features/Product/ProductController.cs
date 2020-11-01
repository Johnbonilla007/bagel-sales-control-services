using System;
using System.Threading.Tasks;
using bagel_sales_control.Core;
using Microsoft.AspNetCore.Mvc;

namespace bagel_sales_control.Features.Product
{
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            if (productService == null) throw new ArgumentNullException(nameof(productService));

            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetProducts()
        {
            Task<Response> products = _productService.GetProducts();

            return Ok(products);
        }

        [HttpGet]
        [Route("by-name")]
        public IActionResult GetProducts([FromQuery] ProductRequest productRequest)
        {
            Task<Response> products = _productService.GetProductByName(productRequest);

            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        public IActionResult SaveProduct([FromBody] ProductRequest productRequest)
        {
            Task<Response> response = _productService.SaveProduct(productRequest);

            return Ok(response);
        }

        [HttpDelete]
        [Route("")]
        public IActionResult DeleteProduct([FromBody] ProductRequest productRequest)
        {
            Task<Response> response = _productService.DeleteProduct(productRequest);

            return Ok(response);
        }

    }
}