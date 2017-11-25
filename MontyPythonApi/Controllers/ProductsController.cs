using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MontyPythonApi.Models;

namespace MontyPythonApi.Controllers
{
	public class ProductsController : ApiController
	{ //https://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api
		Product[] products = new Product[]
		{
			new Product {Id = 1, Name = "Rabid Bunny Slippers", Category = "Apparel", Price = 39.95M, DiscountPrice = 29.95M },
			new Product {Id = 2, Name = "Black Knight Bobblehead", Category = "Toys", Price = 9.95M },
			new Product {Id = 3, Name = "Tim the Enchanter Stocking", Category = "Holiday", Price = 32.50M },
			new Product {Id = 4, Name = "Not Quite Dead Parrot", Category = "Toys", Price = 15.58M }
		};

		public IEnumerable<Product> GetAllProducts()
		{
			return products;
		}

		public IHttpActionResult GetProduct(int id)
		{
			var product = products.FirstOrDefault((p) => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}
	}
}
