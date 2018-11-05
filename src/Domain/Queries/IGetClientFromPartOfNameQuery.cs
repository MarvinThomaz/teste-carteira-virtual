using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Queries
{
    public interface IGetClientFromPartOfNameQuery
    {
         Task<IEnumerable<GetClientViewModel>> Execute(string name);
    }
}