using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> Add(ProductDTO productDto);
        Task Update(Product product);
        Task Remove(int id);

        Task<IEnumerable<ProductDTO>> GetFilteredProducts(string name, decimal? minPrice, decimal? maxPrice);

    }
}