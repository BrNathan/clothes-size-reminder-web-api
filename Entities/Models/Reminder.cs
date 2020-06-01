using Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Reminder : ReminderBase
    {

        [Column("clothes_size_id")]
        public int ClothesSizeId { get; set; }
    }
}
