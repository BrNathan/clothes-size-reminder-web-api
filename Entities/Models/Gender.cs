using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("gender")]
    public class Gender
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("code")]
        [StringLength(45)]
        public string Code { get; set; }

        [Column("label")]
        [StringLength(100)]
        public string Label { get; set; }
    }
}
