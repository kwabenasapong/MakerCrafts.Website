using MakerCrafts.Website.Models;
using MakerCrafts.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MakerCrafts.Website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService) 
        {
            this.ProductService = productService;
        }
        public JsonFileProductService ProductService { get; }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }
<<<<<<< HEAD


        [Route("Rating")]
        [HttpGet]
        public ActionResult Get(
            [FromQuery] string ProductId,
            [FromQuery] int Rating) 
        {
            ProductService.AddRatings(ProductId, Rating);
            return Ok();
        }
=======
>>>>>>> 8e74042d426e266322783c7ffff6733aa269a6e9
    }
}
