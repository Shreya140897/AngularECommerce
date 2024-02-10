using Core.Interfaces;
using Core.Specifications;
using ECommerceAPI.Data;
using ECommerceAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductType> productTypeRepo,IGenericRepository<ProductBrand> productBrandRepo)
        {
            this._productRepo = productRepo;
            this._productTypeRepo = productTypeRepo;
            this._productBrandRepo = productBrandRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() {
            var spec= new ProductWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec= new ProductWithTypesAndBrandsSpecification(id);
            return await _productRepo.GetEntityWithSpec(spec);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok( await _productBrandRepo.GetAllListAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.GetAllListAsync());
        }
    }
}
