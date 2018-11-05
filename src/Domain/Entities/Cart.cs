using System;

namespace teste_carteira_virtual.Domain.Entities
{
    public class Cart
    {
        public Guid Key { get; set; }
        public string ExternalKey { get; set; }
        public Guid ClientKey { get; set; }
        public Client Client { get; set; }
        public double ChargeValue { get; set; }
        public bool IsActive { get; set; }
    }
}