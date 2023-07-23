using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCart.API.Data;
using eCart.API.Data.DTOs;
using eCart.API.Data.Errors;
using eCart.API.Data.Helpers;
using eCart.API.Data.Specifications;
using eCart.API.Models;
using eCart.API.Services;
using eCart.API.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCart.API.Controllers
{
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper)
        {
            _productRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        // 600 is time to Live in sec
        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts(
            [FromQuery] ProductSpecParams specParams)
        {
            // Generate Specs
            var spec = new ProductsWithTypesAndBrandsSpecification(specParams);
            // Count Spec
            var countSpec = new ProductWithFiltersForCountSpecification(specParams);
            // Total Items
            var totalItems = await _productRepo.CountAsync(countSpec);
            // Total Products
            var products = await _productRepo.ListAsync(spec);
            // Mapping data using Auto Mapper
            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);

            return Ok(new Pagination<ProductToReturnDTO>(specParams.PageIndex, specParams.PageSize, totalItems, data));
        }

        [Cached(600)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            ////Working Code
            //var spec = new ProductsWithTypesAndBrandsSpecification();
            //return await _productRepo.GetEntityWithSpec(spec);
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDTO>(product);
        }

        [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
    }
}