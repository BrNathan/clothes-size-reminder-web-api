using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ExtendedModels
{
    public class ReminderExtended : IEntity
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public int BrandId { get; set; }

        public ClothesSize ClothesSize { get; set; }

        public string Comments { get; set; }
    }
}
