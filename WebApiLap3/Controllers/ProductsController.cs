using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCRUD.Models;

namespace WebApiCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id= 1006469,
                Name = "Austing and Barbeque AABQ Wifi Food Thermometer",
                Description = "Thermometer med WiFi för en ptimal innertemperatur",
                Price = 399
            },
            new Product
            {
                Id= 1009334,
                Name = "Andersson Elektrisk Tändare ECL 1.1",
                Description = "Elektrisk stormsäker tändare helt utan gas och bränsle",
                Price = 89
            },
            new Product
            {
                Id= 1002266,
                Name = "Weber Non-Stick Spray",
                Description = "BBQ-oljespray som motverkar att råvaror fastnar på gallret",
                Price = 99
            }
        };

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var product = products.Find(product => product.Id == id);
            return product;
        }

        [HttpPost]
        public void Post([FromBody] Product product)
        {
            products.Add(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var product = products.Where(p => p.Id == id);
            products = products.Except(product).ToList();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            var ExistingProduct = products.Where(p => p.Id == id);
            products = products.Except(ExistingProduct).ToList();

            products.Add(product);
        }
    }
}
