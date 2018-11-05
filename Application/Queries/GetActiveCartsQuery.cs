using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Queries
{
    public class GetActiveCartsQuery : IGetActiveCartsQuery
    {
        private readonly ICartRepository _repository;

        public GetActiveCartsQuery(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetCartViewModel>> Execute()
        {
            var result = await _repository.GetCartsByStatus(true);

            return result.Select(c => new GetCartViewModel
            {
                Key = c.Key,
                ExternalKey = c.ExternalKey,
                ChargeValue = c.ChargeValue,
                ClientDocumentId = c.Client.DocumentId
            });
        }
    }
}