using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("reminder")]
    public class Reminder
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("brand_id")]
        public int BrandId { get; set; }

        [Column("clothes_size_id")]
        public int ClothesSizeId { get; set; }

        [Column("comment")]
        public string Comments { get; set; }
    }
}
