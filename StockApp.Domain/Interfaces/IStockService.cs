using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IStockService
    {
        Task AutomaticReplacement(Product? product);
    }
}
