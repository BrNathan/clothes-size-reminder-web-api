using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("clothes_size")]
    public class ClothesSize : IEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("clothes_id")]
        public int ClothesId { get; set; }

        [Column("size_id")]
        public int SizeId { get; set; }
    }
}
