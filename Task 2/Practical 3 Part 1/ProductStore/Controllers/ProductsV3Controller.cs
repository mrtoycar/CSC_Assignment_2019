using ProductStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductStore.Controllers
{

    public class ProductsV3Controller : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        [HttpGet]
        [Route("api/v3/products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }


        [HttpGet]
        [Route("api/v3/products/{id:int:min(2)}")]
        public Product GetProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }


        [HttpGet]
        //[Route("api/v3/products")]
        [Route("api/v3/products", Name = "getProductByCategoryV3")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }


        [HttpPost]
        [Route("api/v3/products")]
        public HttpResponseMessage PostProduct(Product item)
        {
            if (ModelState.IsValid)
            {
                item = repository.Add(item);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

                //string uri = Url.Link("DefaultApi", new { id = item.Id });
                //response.Headers.Location = new Uri(uri);
                return response;
            } else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("api/v3/products")]
        public void PutProduct(Product product)
        {
            //product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

        }

        [HttpDelete]
        [Route("api/v3/products/{id}")]
        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }


    }
}
