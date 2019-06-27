using WebAPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebAPI2.Controllers
{
    public class ProductsV1Controller : ApiController
    {
        List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        [HttpGet]
        [Route("api/v1/products/version")]
        public string[] GetVersion()
        {
            return new string[]
              {"hello",
                  "version 2",
                  "2"
              };
        }

        [HttpGet]
        [Route("api/v1/products/message")]
        public HttpResponseMessage GetMultipleNames(string name1, string name2, string name3)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent("<html><body>Hello World " +
                    " name1 =" + name1 +
                    " name2= " + name2 +
                    " name3=" + name3 +
                    " is provided in path parameter</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpGet]
        //http://localhost:9000/api/v1/products
        [Route("api/v1/products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpGet]
        [Route("api/v1/products/{id:int:min(2)}")]
        //http://localhost:9000/api/v1/products/3
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