using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TakeHomeAssignmentAPI.Services;
using TakeHomeAssignmentAPI.Models;
using TakeHomeAssignmentAPI.DTO;

namespace TakeHomeAssignmentAPI.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("2.0")]
    public class ProductControllerV2 : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductControllerV2(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Retrieves products along with their packaging hierarchy.
        /// </summary>
        [HttpGet("with-packaging")]
        public async Task<ActionResult<IEnumerable<ProductWithPackagingDto>>> GetProductsWithPackagingAsync()
        {
            var productsWithPackaging = await _productService.GetProductsWithPackagingAsync();
            return Ok(productsWithPackaging);
        }
    }
}
