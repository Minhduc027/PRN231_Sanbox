using DEMO_ASP_.NET_CORE_Web_API.Data;
using DEMO_ASP_.NET_CORE_Web_API.Dtos;
using DEMO_ASP_.NET_CORE_Web_API.Helper;
using DEMO_ASP_.NET_CORE_Web_API.Interface;
using DEMO_ASP_.NET_CORE_Web_API.Model;
using Microsoft.EntityFrameworkCore;

namespace DEMO_ASP_.NET_CORE_Web_API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            var stockExist = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == stockModel.Symbol);
            if(stockExist != null)
            {
                throw new Exception("Stock already existed!");
            }
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStockAsync(long id) 
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }
            var comments = await _context.Comments
                      .Where(c => c.StockId == id)
                      .ToListAsync();
            if(comments.Count > 0)
            {
                _context.Comments.RemoveRange(comments);
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel ?? null;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stock = _context.Stocks.Include(c => c.Comments).AsQueryable();
            stock = ApplyFilters(stock, query);
            stock = ApplySorting(stock, query);
            return await stock.ToListAsync();
        }

        private IQueryable<Stock> ApplyFilters(IQueryable<Stock> stock, QueryObject query)
        {
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
                stock = from s in stock
                        where s.CompanyName.Contains(query.CompanyName)
                        select s;

            if (!string.IsNullOrWhiteSpace(query.Symbol))
                stock = from s in stock
                        where s.Symbol.Contains(query.Symbol)
                        select s;

            if (!string.IsNullOrWhiteSpace(query.Industry))
                stock = from s in stock
                        where s.Industry.Contains(query.Industry)
                        select s;

            return stock;
        }

        private IQueryable<Stock> ApplySorting(IQueryable<Stock> stock, QueryObject query)
        {
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    stock = query.isDescending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                    stock = query.isDescending ? stock.OrderByDescending(s => s.CompanyName) : stock.OrderBy(s => s.CompanyName);
            }

            return stock;
        }
        public async Task<Stock?> GetByIdAsync(long id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> StockExist(long id)
        {
            return await _context.Stocks.AnyAsync(x => x.Id == id);
        }
        
        public async Task<Stock?> UpdateStockAsync(long id, UpdateStockDto dto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = dto.Symbol;
            stockModel.CompanyName = dto.CompanyName;
            stockModel.Purchase = dto.Purchase;
            stockModel.LastDiv = dto.LastDiv;
            stockModel.MarketCap = dto.MarketCap;
            stockModel.Industry = dto.Industry;
            await _context.SaveChangesAsync();
            return stockModel;
        }

    }
}
