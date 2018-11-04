using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Entities;

namespace teste_carteira_virtual.Domain.Repositories
{
    public interface ICartRepository
    {
         Task AddCart(Cart cart);
         Task UpdateCartValue(string externalKey, double value);
         Task<Cart> GetActiveCartFromClient(string documentId);
         Task<Cart> GetCartFromExternalKey(string externalKey);
         Task<IEnumerable<Cart>> GetCartsByStatus(bool isActive);
         Task UpdateCartStatus(string externalKey, bool isActive);
    }
}