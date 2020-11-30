using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blackstore_firebase_api.Configuration;
using blackstore_firebase_api.Entity;


namespace blackstore_firebase_api.Controllers
{
    [ApiController]
    public class ProductsController : Controller
    {
        FirebaseConfiguration instance = FirebaseConfiguration.Instance;

        [HttpGet("/api")]
        public async Task<List<Product>> GetAll()
        {
            return await instance.GetAllProducts();
        }

        [HttpGet("/api/search")]
        public async Task<Result> Search([FromQuery]String q)
        {
            return await instance.GetProductWhereName(q);
        }
 
        [HttpGet("/api/item/{id}")]
        public async Task<Product> DetailProduct (string id)
        {
            return await instance.GetProductById(id);
        }


        [HttpPost("/api")]
        public async Task<Product> Post([FromBody] Product product)
        {
            return await instance.AddProduct(product);
        }


        [HttpDelete("/api/{id}")]
        public async Task<String> Delete(String id)
        {
            return await instance.DeleteProductById(id);
        }

        /*// PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] string value)
        {
        }*/


    }
}
