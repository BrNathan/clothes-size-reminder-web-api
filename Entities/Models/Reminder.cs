using Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Reminder : ReminderBase
    {

        [Column("clothes_size_id")]
        public int ClothesSizeId { get; set; }
    }
}
