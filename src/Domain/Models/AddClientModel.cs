using System.ComponentModel.DataAnnotations;

namespace teste_carteira_virtual.Domain.Models
{
    public class AddClientModel
    {
        [Required]
        public string DocumentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(\(11\) (9\d{4})-\d{4})|((\(1[2-9]{1}\)|\([2-9]{1}\d{1}\)) [5-9]\d{3}-\d{4})$")]
        public string Phone { get; set; }

        [RegularExpression(@"^(\(11\) (9\d{4})-\d{4})|((\(1[2-9]{1}\)|\([2-9]{1}\d{1}\)) [5-9]\d{3}-\d{4})$")]
        public string SecondPhone { get; set; }

        [Required]
        public AddAddressModel Address { get; set; }
    }
}