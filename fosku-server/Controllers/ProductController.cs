using fosku_server.DTO.Product;
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
        [Route("/product")]
        public ActionResult GetProduct([FromQuery]int id)
        {
            //TODO: check id properly ??
            Product? product = productService.GetProduct(id);
            if (product == null)
            {
                return NotFound(id);
            }
            return Ok(product);
        }

        //TODO: add auth logic
        [HttpPost]
        [Route("/product")]
        public ActionResult CreateProduct([FromBody] CreateProductRequest productRequest)
        {
            Product product = new ();
            (product.Name, product.Description, product.Price, product.StockQuantity) = productRequest;
            productService.CreateProduct(product);
            return Created();
        }

        [HttpPut]
        [Route("/product")]
        public ActionResult UpdateProduct([FromBody] UpdateOrInsertProductRequest request)
        {
            Product product = new ();
            (product.Id, product.Name, product.Description, product.Price, product.StockQuantity) = request;
            productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Route("/product")]
        public ActionResult DeleteProduct([FromBody] int id)
        {
            System.Console.WriteLine($"Deleting Product by id: {id}");
            productService.DeleteProductById(id);
            return Ok();
        }
    }
}
