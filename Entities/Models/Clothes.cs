using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("clothes")]
    public class Clothes : IEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("label")]
        public string Label { get; set; }

        [Required]
        [Column("clothes_category_id")]
        public int ClothesCategoryId { get; set; }

        [Required]
        [Column("gender_id")]
        public int? GenderId { get; set; }
    }
}
