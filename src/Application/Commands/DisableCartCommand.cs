using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Commands
{
    public class DisableCartCommand : IDisableCartCommand
    {
        private readonly ICartRepository _repository;

        public DisableCartCommand(ICartRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ValidationResponse> Validations { get; set; }

        public async Task<GetCartViewModel> Execute(string externalKey)
        {
            var cart = await _repository.GetCartFromExternalKey(externalKey);

            if(cart == null)
            {
                Validations = new List<ValidationResponse>
                {
                    new ValidationResponse
                    {
                        Type = ResponseType.NotFoundedObject,
                        Property = nameof(externalKey)
                    }
                };

                return null;
            }

            await _repository.UpdateCartStatus(externalKey, false);

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