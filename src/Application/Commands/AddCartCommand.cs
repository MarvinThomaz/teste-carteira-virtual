using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using teste_carteira_virtual.Application.Abstractions;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Entities;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Commands
{
    public class AddCartCommand : IAddCartCommand
    {
        private readonly ICartRepository _cartRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IModelStateAccessor _modelStateAccessor;

        public AddCartCommand(ICartRepository repository, IClientRepository clientRepository, IModelStateAccessor modelStateAccessor)
        {
            _cartRepository = repository;
            _clientRepository = clientRepository;
            _modelStateAccessor = modelStateAccessor;
        }

        public AddCartModel Model { get; set; }
        public IEnumerable<ValidationResponse> Validations { get; set; }

        public async Task<GetCartViewModel> Execute()
        {
            var validations = _modelStateAccessor.ModelState.ValidateModel();

            if(validations.Any())
            {
                return SetValidationsAndReturn(validations);
            }
            else
            {
                var cart = CreateCart();

                cart.Client = await _clientRepository.GetClientFromDocumentId(Model.Client.DocumentId);
                
                if(cart.Client == null)
                {
                    CreateClient(cart);
                }

                cart.ClientKey = cart.Client.Key;

                await _cartRepository.AddCart(cart);

                return new GetCartViewModel
                {
                    Key = cart.Key,
                    ExternalKey = cart.ExternalKey,
                    ChargeValue = cart.ChargeValue,
                    ClientDocumentId = cart.Client.DocumentId
                };
            }
        }

        private GetCartViewModel SetValidationsAndReturn(IEnumerable<ValidationResponse> validations)
        {
            Validations = validations;

            return null;
        }

        private Cart CreateCart()
        {
            return new Cart
            {
                Key = Guid.NewGuid(),
                ExternalKey = Model.ExternalKey,
                ChargeValue = Model.ChargeValue,
                IsActive = true
            };
        }

        private void CreateClient(Cart cart)
        {
            cart.Client = new Client
            {
                Key = Guid.NewGuid(),
                DocumentId = Model.Client.DocumentId,
                Name = Model.Client.Name,
                Phone = Model.Client.Phone,
                SecondPhone = Model.Client.SecondPhone,
                Address = new Address
                {
                    Key = Guid.NewGuid(),
                    Street = Model.Client.Address.Street,
                    State = Model.Client.Address.State,
                    City = Model.Client.Address.City,
                    Complement = Model.Client.Address.Complement,
                    Country = Model.Client.Address.Country,
                    PostalCode = Model.Client.Address.PostalCode,
                    Number = Model.Client.Address.Number
                }
            };

            cart.Client.AddressKey = cart.Client.Address.Key;
        }
    }
}