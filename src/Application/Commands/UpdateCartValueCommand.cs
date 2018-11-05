using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Commands
{
    public class UpdateCartValueCommand : IUpdateCartValueCommand
    {
        private readonly ICartRepository _repository;

        public IUpdateCartValueModel Model { get; set; }
        public IEnumerable<ValidationResponse> Validations { get; set; }

        public UpdateCartValueCommand(ICartRepository repository)
        {
            _repository = repository;
        }

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

            await _repository.UpdateCartValue(externalKey, Model.ChargeValue.Value);

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