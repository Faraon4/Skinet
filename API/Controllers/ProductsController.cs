using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }


        // Get the Product(s)

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetPRoduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }

        // Get the Brand(s)
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _repo.GetProductBrandsAsync();
            return Ok(brands);
        }



        // Get the Type(s)
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductBrand>>> GetTypes()
        {
            var types = await _repo.GetProductTypesAsync();
            return Ok(types);
        }
    }
}