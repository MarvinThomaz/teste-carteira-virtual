using System.ComponentModel.DataAnnotations;

namespace teste_carteira_virtual.Domain.Models
{
    public class AddCartModel
    {
        [Required]
        public string ExternalKey { get; set; }

        [Required]
        public AddClientModel Client { get; set; }
        
        [Required]
        public double ChargeValue { get; set; }
    }
}