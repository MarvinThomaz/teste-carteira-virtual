using System;

namespace teste_carteira_virtual.Domain.Models
{
    public class GetCartViewModel
    {
        public Guid Key { get; set; }
        public string ExternalKey { get; set; }
        public string ClientDocumentId { get; set; }
        public double ChargeValue { get; set; }
    }
}