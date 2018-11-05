using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Entities;
using teste_carteira_virtual.Domain.Repositories;
using teste_carteira_virtual.Infrastructure.Context;

namespace teste_carteira_virtual.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext _context;
        private readonly IPagingParametersAccessor _pagingParametersAccessor;

        public CartRepository(DatabaseContext context, IPagingParametersAccessor pagingParametersAccessor)
        {
            _context = context;
            _pagingParametersAccessor = pagingParametersAccessor;
        }

        public async Task AddCart(Cart cart)
        {
            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetActiveCartFromClient(string documentId)
        {
            return await _context
                    	    .Set<Cart>()
                            .Include(c => c.Client)
                            .FirstOrDefaultAsync(c => c.Client.DocumentId == documentId);
        }

        public async Task<Cart> GetCartFromExternalKey(string externalKey)
        {
            return await _context
                    	    .Set<Cart>()
                            .Include(c => c.Client)
                            .FirstOrDefaultAsync(c => c.ExternalKey == externalKey);
        }

        public async Task<IEnumerable<Cart>> GetCartsByStatus(bool isActive)
        {
            var result = await _context
                    	    .Set<Cart>()
                            .Include(c => c.Client)
                            .Where(c => c.IsActive == isActive)
                            .Take(_pagingParametersAccessor.RecordsPerPage)
                            .Skip(_pagingParametersAccessor.Skip)
                            .ToListAsync();

            _pagingParametersAccessor.TotalItems = result?.Count ?? 0;

            return result;
        }

        public async Task UpdateCartStatus(string externalKey, bool isActive)
        {
            var cart = await _context.Set<Cart>().FirstOrDefaultAsync(c => c.ExternalKey == externalKey);

            cart.IsActive = isActive;

            _context.Entry(cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartValue(string externalKey, double value)
        {
            var cart = await _context.Set<Cart>().FirstOrDefaultAsync(c => c.ExternalKey == externalKey);

            cart.ChargeValue = value;

            _context.Entry(cart).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}