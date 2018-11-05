using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using teste_carteira_virtual.Domain.Entities;
using teste_carteira_virtual.Domain.Repositories;
using teste_carteira_virtual.Infrastructure.Context;

namespace teste_carteira_virtual.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _context;

        public ClientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Client> GetClientFromDocumentId(string documentId)
        {
            return await _context.Set<Client>().FirstOrDefaultAsync(c => c.DocumentId == documentId);
        }

        public async Task<IEnumerable<Client>> GetClientFromPartOfName(string name)
        {
            var clients = await _context.Set<Client>().ToListAsync();

            return clients.Where(c => c.Name.Contains(name));
        }
    }
}