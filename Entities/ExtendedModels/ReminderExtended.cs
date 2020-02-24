using Entities.Base;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ExtendedModels
{
    public class ReminderExtended : ReminderBase
    {
        public ClothesSize ClothesSize { get; set; }
    }
}
