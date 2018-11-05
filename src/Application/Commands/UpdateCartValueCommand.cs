using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Commands
{
    public class UpdateCartValueCommand : IUpdateCartValueCommand
    {
        private readonly ICartRepository _repository;

        public IUpdateCartValueModel Model { get; set; }

        public UpdateCartValueCommand(ICartRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCartViewModel> Execute(string externalKey)
        {
            await _repository.UpdateCartValue(externalKey, Model.ChargeValue.Value);

            var cart = await _repository.GetCartFromExternalKey(externalKey);

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