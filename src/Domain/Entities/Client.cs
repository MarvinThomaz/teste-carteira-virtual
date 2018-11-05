using System;
using System.Collections.Generic;

namespace teste_carteira_virtual.Domain.Entities
{
    public class Client
    {
        public Guid Key { get; set; }
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string SecondPhone { get; set; }
        public Guid AddressKey { get; set; }
        public Address Address { get; set; }

        public List<Cart> Carts { get; set; }
    }
}