using System.ComponentModel.DataAnnotations;

namespace teste_carteira_virtual.Domain.Models
{
    public class AddAddressModel
    {
        [Required]
        public string Street { get; set; }

        public string Complement { get; set; }
        
        [Required]
        public int Number { get; set; }

        [Required]
        [RegularExpression("[0-9]{5}-[0-9]{3}")]
        public string PostalCode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }
    }
}