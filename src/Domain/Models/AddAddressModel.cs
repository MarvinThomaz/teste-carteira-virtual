using System.ComponentModel.DataAnnotations;

namespace teste_carteira_virtual.Domain.Models
{
    public class AddAddressModel
    {
        [Required]
        [MaxLength(75)]
        [MinLength(2)]
        public string Street { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        public string Complement { get; set; }
        
        [Required]
        public int Number { get; set; }

        [Required]
        [RegularExpression("[0-9]{5}-[0-9]{3}")]
        [MaxLength(9)]
        [MinLength(9)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(75)]
        [MinLength(2)]
        public string City { get; set; }

        [Required]
        [MaxLength(75)]
        [MinLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(75)]
        [MinLength(2)]
        public string Country { get; set; }
    }
}