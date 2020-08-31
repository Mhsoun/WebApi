using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiL4.Models;

namespace WebApiL4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id=1006368,
                Name="Austin and Barbeque AABQ wifi Food Thermometer",
                Description="Termometer med WiFi för en optimal innertemeratur",
                Price=399
            },
            new Product
            {
                Id=1009334,
                Name="Andersson Elektrisk Tänare ECL 1.1",
                Description="Elektrisk stormsäler tändare helt utan gas och bränsle",
                Price=89
            },
            new Product
            {
                Id=1002266,
                Name="Weber Non-Stick Spray",
                Description="BBQ-oljespray som motverkar att råvaror fastnar på gallret",
                Price=99
            }
        };

        //GET ALL PRODUCTS
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            return products;
        }

        //GET SPECIFIC PRODUCT
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.Find(p => p.Id == id);

            if(product==null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (products.Exists(p => p.Id == product.Id))
            {
                return Conflict();
            }
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, products);
        }

        //DELETE PRODUCT
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Product>> Delete(int id)
        {
            var product = products.Where(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }

            products = products.Except(product).ToList();
            return products;
        }

        //UPDATE PRODUCT
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Product>> Put(int id, [FromBody] Product product)
        {
            if(id !=product.Id)
            {
                return BadRequest();
            }

            var existingProduct = products.Where(p => p.Id == id);
            products.Add(product);
            return products;
        }
    }
}
