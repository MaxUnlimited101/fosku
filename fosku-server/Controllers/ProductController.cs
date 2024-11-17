using fosku_server.Models;
using fosku_server.Services.Auth;
using fosku_server.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace fosku_server.Controllers
{
    [ApiController()]
    [Route("/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IAuthService authService;
        public ProductController(IProductService productService, IAuthService authService)
        {
            this.productService = productService;
            this.authService = authService;
        }

        [HttpGet]
        [Route("/products")]
        public ActionResult GetProducts()
        {
            return Ok(productService.GetProducts());
        }

        [HttpGet]
        public ActionResult GetProduct([FromQuery]int id)
        {
            //TODO: check id
            Product? product = productService.GetProduct(id);
            if (product == null)
            {
                return NotFound(id);
            }
            return Ok(product);
        }
    }
}
