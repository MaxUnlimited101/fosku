using fosku_server.DTO.Product;
using fosku_server.Models;
using fosku_server.Services.Auth;
using fosku_server.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fosku_server.Controllers
{
    [ApiController()]
    [Route("/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IAuthService authService;
        public static readonly string imagesDirectory = "/images";
        public ProductController(IProductService productService, IAuthService authService)
        {
            this.productService = productService;
            this.authService = authService;
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }
        }

        [HttpGet]
        [Route("/products")]
        public ActionResult GetProducts()
        {
            return Ok(productService.GetProducts());
        }

        [HttpGet]
        [Route("/product/{id}")]
        public ActionResult GetProduct([FromRoute] int id)
        {
            //TODO: check id properly ??
            Product? product = productService.GetProduct(id);
            if (product == null)
            {
                return NotFound(id);
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("/product")]
        [Authorize]
        public ActionResult CreateProduct([FromBody] CreateProductRequest request)
        {
            Product product = new();
            (product.Name, product.Description, product.Price, product.StockQuantity, product.LogoAltText) = request;
            product.LogoUrl = "";

            productService.CreateProduct(product);
            return Ok(product.Id);
        }

        [HttpPost]
        [Authorize]
        [Route("/image/{id}")]
        public ActionResult CreateImage([FromForm] IFormFile logoImage, [FromRoute] int id)
        {
            Product? p = productService.GetProduct(id);
            if (p == null)
            {
                return BadRequest($"No product with id {id} exists");
            }
            string fileName = $"{Guid.NewGuid()}_{DateTime.Now:yyyyMMddHHmmssfff}{Path.GetExtension(logoImage.FileName)}";
            p.LogoUrl = Path.Combine(imagesDirectory, fileName);
            using (var stream = new FileStream(p.LogoUrl, FileMode.Create))
            {
                logoImage.CopyTo(stream);
            }
            productService.UpdateProduct(p);
            return Ok();
        }

        [HttpGet]
        [Route("/images/{fileName}")]
        public ActionResult GetImage([FromRoute] string fileName)
        {
            string fullPath = Path.Combine(imagesDirectory, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                var bytes = System.IO.File.ReadAllBytes(fullPath);
                return File(bytes, "image/*");
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize]
        [Route("/product")]
        public ActionResult UpdateProduct([FromBody] UpdateOrInsertProductRequest request)
        {
            Product product = new();
            (product.Id, product.Name, product.Description, product.Price, product.StockQuantity, product.LogoUrl, product.LogoAltText) = request;
            productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("/product")]
        public ActionResult DeleteProduct([FromBody] DeleteProductRequest request)
        {
            System.Console.WriteLine($"Deleting Product by id: {request.Id}");
            productService.DeleteProductById(request.Id);
            return Ok();
        }
    }
}
