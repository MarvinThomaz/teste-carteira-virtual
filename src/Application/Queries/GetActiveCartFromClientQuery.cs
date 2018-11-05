using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Queries
{
    public class GetActiveCartFromClientQuery : IGetActiveCartFromClientQuery
    {
        private readonly ICartRepository _repository;

        public IEnumerable<ValidationResponse> Validations { get; set; }

        public GetActiveCartFromClientQuery(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCartViewModel> Execute(string documentId)
        {
            var cart = await _repository.GetActiveCartFromClient(documentId);

            if(cart == null)
            {
                Validations = new List<ValidationResponse>
                {
                    new ValidationResponse
                    {
                        Type = ResponseType.NotFoundedObject,
                        Property = nameof(documentId)
                    }
                };

                return null;
            }

            return new GetCartViewModel
            {
                Key = cart.Key,
                ExternalKey = cart.ExternalKey,
                ChargeValue = cart.ChargeValue,
                ClientDocumentId = cart.Client.DocumentId
            };
        }
    }
}