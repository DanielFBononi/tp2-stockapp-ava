using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> Add(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.Create(productEntity);
            return productEntity;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntity = await _productRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<Product> GetProductById(int id)
        {
            var productEntity = await _productRepository.GetById(id);
            if (productEntity is null)
            {
                throw new ArgumentException();
            }

            return productEntity;
        }

        public async Task Remove(int id)
        {
            var productEntity = _productRepository.GetById(id).Result;
            await _productRepository.Remove(productEntity);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetFilteredProducts(string name, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.GetFilteredProducts(name, minPrice, maxPrice);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}