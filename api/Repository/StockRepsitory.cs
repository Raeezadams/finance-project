using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepsitory : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepsitory(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Contains(queryObject.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Contains(queryObject.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDecsending ? stocks.OrderByDescending( X => X.Symbol ) : stocks.OrderBy( X => X.Symbol );
                }
            }

            var skipNumber = (queryObject.PageNumber -1) * queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);
        }

        public Task<bool> StockExsists(int id)
        {
            return _context.Stock.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto SockDto)
        {
            var exsistingStock = await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(x =>x.Id == id);
            if(exsistingStock == null)
            {
                return null;
            }

            exsistingStock.Symbol = SockDto.Symbol;
            exsistingStock.Purchase = SockDto.Purchase;
            exsistingStock.MarketCap = SockDto.MarketCap;
            exsistingStock.LastDiv = SockDto.LastDiv;
            exsistingStock.CompanyName = SockDto.CompanyName;
            exsistingStock.Industry = SockDto.Industry;

            await _context.SaveChangesAsync();

            return exsistingStock;
        }
    }
}
