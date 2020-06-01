using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("gender")]
    public class Gender : IEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("code")]
        [StringLength(45)]
        public string Code { get; set; }

        [Column("label")]
        [StringLength(100)]
        public string Label { get; set; }
    }
}
