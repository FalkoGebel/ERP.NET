using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErpDotNet.Repository
{
    public class Item
    {
        // TODO - check, if it is possible to validate user inputs against the MaxLength attribute

        [Key]
        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Number { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; } = string.Empty;
    }
}