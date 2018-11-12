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
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _context;
        private readonly IPagingParametersAccessor _pagingParametersAccessor;

        public ClientRepository(DatabaseContext context, IPagingParametersAccessor pagingParametersAccessor)
        {
            _context = context;
            _pagingParametersAccessor = pagingParametersAccessor;
        }

        public async Task<Client> GetClientFromDocumentId(string documentId)
        {
            return await _context
                            .Set<Client>()
                            .Include(C => C.Address)
                            .FirstOrDefaultAsync(c => c.DocumentId == documentId);
        }

        public async Task<IEnumerable<Client>> GetClientFromPartOfName(string name)
        {
            var result = await _context
                            .Set<Client>()
                            .Include(c => c.Address)
                            .Where(c => string.IsNullOrEmpty(name) || c.Name.ToLower().Contains(name.ToLower()))
                            .Take(_pagingParametersAccessor.RecordsPerPage)
                            .Skip(_pagingParametersAccessor.Skip)
                            .ToListAsync();

            _pagingParametersAccessor.TotalItems = result?.Count ?? 0;

            return result;
        }
    }
}