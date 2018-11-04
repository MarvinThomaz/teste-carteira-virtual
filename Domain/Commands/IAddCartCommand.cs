using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Commands
{
    public interface IAddCartCommand
    {
         AddCartModel Model { get; set; }

         Task<GetCartViewModel> Execute();
    }
}