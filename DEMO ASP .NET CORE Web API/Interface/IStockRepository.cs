using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using DEMO_ASP_.NET_CORE_Web_API.Model;

namespace DEMO_ASP_.NET_CORE_Web_API.Interface
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(long id);
        Task<Stock> CreateStockAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(long id, UpdateStockDto dto);
        Task<Stock?> DeleteStockAsync(long id);
        Task<bool> StockExist(long id);
    }
}
