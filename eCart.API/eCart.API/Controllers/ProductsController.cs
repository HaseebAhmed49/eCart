using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCart.API.Data;
using eCart.API.Models;
using eCart.API.Services;
using eCart.API.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;


        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
        {
            _productRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepo.ListAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _productRepo.GetByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}