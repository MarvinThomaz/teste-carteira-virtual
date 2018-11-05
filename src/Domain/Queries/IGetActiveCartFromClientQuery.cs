using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Queries
{
    public interface IGetActiveCartFromClientQuery
    {
        IEnumerable<ValidationResponse> Validations { get; set; }

        Task<GetCartViewModel> Execute(string documentId);
    }
}