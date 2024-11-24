using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using static StackExchange.Redis.Role;

namespace StockApp.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IProductService _productService;
        private IMapper _mapper;
        public StockService(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task AutomaticReplacement(Product? product)
        {
            product.Stock += 50;
            await _productService.Update(product);
        }

    }
}
