using System;

namespace teste_carteira_virtual.Domain.Entities
{
    public class Address
    {
        public Guid Key { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public int Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}