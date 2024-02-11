using API.DTO;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,IMapper mapper)
        {
            this._productRepo = productRepo;
            this._productTypeRepo = productTypeRepo;
            this._productBrandRepo = productBrandRepo;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams) {
            var spec= new ProductWithTypesAndBrandsSpecification(productSpecParams);
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec= new ProductWithTypesAndBrandsSpecification(id);
            var product= await _productRepo.GetEntityWithSpec(spec);
            if (product == null) return NotFound();
            return _mapper.Map<Product,ProductDto>(product);
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
