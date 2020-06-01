using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("brand")]
    public class Brand : IEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("corporate_name")]
        public string CorporateName { get; set; }

        [Column("is_validated")]
        public bool IsValidated { get; set; }
    }
}
