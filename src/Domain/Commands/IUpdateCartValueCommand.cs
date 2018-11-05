using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Commands
{
    public interface IUpdateCartValueCommand
    {
         IUpdateCartValueModel Model { get; set; }

         Task<GetCartViewModel> Execute(string externalKey);
    }
}