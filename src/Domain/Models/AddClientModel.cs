using System.ComponentModel.DataAnnotations;

namespace teste_carteira_virtual.Domain.Models
{
    public class AddClientModel
    {
        [Required]
        [MaxLength(14)]
        [MinLength(14)]
        [RegularExpression(@"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})")]
        public string DocumentId { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(\(11\) (9\d{4})-\d{4})|((\(1[2-9]{1}\)|\([2-9]{1}\d{1}\)) [5-9]\d{3}-\d{4})$")]
        [MaxLength(15)]
        [MinLength(15)]
        public string Phone { get; set; }

        [RegularExpression(@"^(\(11\) (9\d{4})-\d{4})|((\(1[2-9]{1}\)|\([2-9]{1}\d{1}\)) [5-9]\d{3}-\d{4})$")]
        [MaxLength(15)]
        [MinLength(15)]
        public string SecondPhone { get; set; }

        [Required]
        public AddAddressModel Address { get; set; }
    }
}