namespace teste_carteira_virtual.Domain.Models
{
    public class UpdateCartModel : IUpdateCartValueModel
    {
        public double? ChargeValue { get; set; }
        public bool? IsActive { get; set; }
    }
}