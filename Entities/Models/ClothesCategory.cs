﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("clothes_category")]
    public class ClothesCategory : IEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("label")]
        public string Label { get; set; }

        [Column("is_validated")]
        public bool IsValidated { get; set; }
    }
}
