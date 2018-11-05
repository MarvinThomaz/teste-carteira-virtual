using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Repositories;

namespace teste_carteira_virtual.Application.Queries
{
    public class GetClientFromPartOfNameQuery : IGetClientFromPartOfNameQuery
    {
        private readonly IClientRepository _repository;

        public GetClientFromPartOfNameQuery(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetClientViewModel>> Execute(string name)
        {
            var clients = await _repository.GetClientFromPartOfName(name);

            return clients.Select(c => new GetClientViewModel
            {
                Key = c.Key,
                DocumentId = c.DocumentId,
                Name = c.Name,
                Phone = c.Phone,
                SecondPhone = c.SecondPhone,
                Address = new GetAddressViewModel
                {
                    Key = c.Address.Key,
                    Street = c.Address.Street,
                    Complement = c.Address.Complement,
                    Number = c.Address.Number,
                    PostalCode = c.Address.PostalCode,
                    City = c.Address.City,
                    State = c.Address.State,
                    Country = c.Address.Country
                }
            });
        }
    }
}