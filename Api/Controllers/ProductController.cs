using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        [HttpGet("{id}")]
        public ActionResult GetProduct(int id)
        {
            var product = new Product
            {
                Id = id,
                Code = "0123456789",
                Description = "Cerveja",
                Price = 2.99M,
            };

            return Ok(product);
        }

        [HttpPost("register")]
        public ActionResult Product(Product newProduct)
        {
            var product = new Product
            {
                Id = 1,
                Code = newProduct.Code,
                Description = newProduct.Description,
                Price = newProduct.Price,
            };

            return Ok(product);
        }
    }
}