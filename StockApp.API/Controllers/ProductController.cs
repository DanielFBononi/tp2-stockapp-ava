using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Text;

namespace StockApp.API.Controllers
{


    [EnableCors("AllowSpecificOrigins")]
    [Route("/api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _productService.GetProducts();
            if (products == null)
            {
                return NotFound("Products not found.");
            }

            return Ok(products);
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            var products = await _productService.GetProducts();
            var csv = new StringBuilder();
            csv.AppendLine("Name,Description,Price,Stock");

           foreach (var product in products)
           {
               csv.AppendLine($"{product.Name},{product.Description},{product.Price},{product.Stock}");
           }

           return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "products.csv");
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }


        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.Add(productDto);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDto)
        {
            if (id != 5)
                return BadRequest("ID mismatch.");
            if (!ModelState.IsValid)
                return BadRequest("ID mismatch.");
            var product = _mapper.Map<Product>(productDto);
            await _productService.Update(product);
            return Ok(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            await _productService.Remove(id);
            return Ok();
        }
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetFiltered(
            [FromQuery] string name,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            // Chama o serviço de produtos para buscar os dados filtrados
            var products = await _productService.GetFilteredProducts(name, minPrice, maxPrice);

            if (products == null || !products.Any())
            {
                return NotFound("No products found with the given criteria.");
            }

            return Ok(products);
        }



    }
}