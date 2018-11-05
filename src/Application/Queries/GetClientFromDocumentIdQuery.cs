using System.Collections.Generic;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Queries
{
    public class GetClientFromDocumentIdQuery : IGetClientFromDocumentIdQuery
    {
        private readonly IClientRepository _repository;

        public IEnumerable<ValidationResponse> Validations { get; set; }

        public GetClientFromDocumentIdQuery(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetClientViewModel> Execute(string documentId)
        {
            var client = await _repository.GetClientFromDocumentId(documentId);

            if(client == null)
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

            return new GetClientViewModel
            {
                Key = client.Key,
                DocumentId = client.DocumentId,
                Name = client.Name,
                Phone = client.Phone,
                SecondPhone = client.SecondPhone,
                Address = new GetAddressViewModel
                {
                    Key = client.Address.Key,
                    Street = client.Address.Street,
                    Complement = client.Address.Complement,
                    Number = client.Address.Number,
                    PostalCode = client.Address.PostalCode,
                    City = client.Address.City,
                    State = client.Address.State,
                    Country = client.Address.Country
                }
            };
        }
    }
}