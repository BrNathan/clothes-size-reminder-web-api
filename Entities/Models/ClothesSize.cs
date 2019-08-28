using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("clothes_size")]
    public class ClothesSize
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("clothes_id")]
        public int ClothesId { get; set; }

        [Column("size_id")]
        public int SizeId { get; set; }
    }
}
