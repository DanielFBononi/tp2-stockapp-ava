using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace StockApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> Create(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetById(int? id)
        {
            return await _productContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            //_productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
        public async Task<IEnumerable<Product>> GetFilteredProducts(string name, decimal? minPrice, decimal? maxPrice)
        {
            var query = _productContext.Set<Product>().AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }
    }
}
