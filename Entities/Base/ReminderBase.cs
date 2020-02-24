using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Base
{
    [Table("reminder")]
    public class ReminderBase : IEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("brand_id")]
        public int BrandId { get; set; }

        [Column("comment")]
        public string Comments { get; set; }

        [Column("sql_creation_datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }
    }
}
