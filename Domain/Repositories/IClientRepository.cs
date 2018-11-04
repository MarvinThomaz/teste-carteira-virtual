using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Entities;

namespace teste_carteira_virtual.Domain.Repositories
{
    public interface IClientRepository
    {
         Task<IEnumerable<Client>> GetClientFromPartOfName(string name);
         Task<Client> GetClientFromDocumentId(string documentId);
    }
}