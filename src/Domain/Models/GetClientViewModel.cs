using System;

namespace teste_carteira_virtual.Domain.Models
{
    public class GetClientViewModel
    {
        public Guid Key { get; set; }
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string SecondPhone { get; set; }
        public GetAddressViewModel Address { get; set; }
    }
}