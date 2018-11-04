using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Queries
{
    public interface IGetCartFromExternalKeyQuery
    {
         Task<GetCartViewModel> Execute(string externalKey);
    }
}