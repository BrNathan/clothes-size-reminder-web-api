using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ExtendedModels
{
    public class ClothesExtended
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Label { get; set; }

        public ClothesCategory ClothesCategory { get; set; }

        public Gender Gender { get; set; }

        public ClothesExtended()
        { }

        public ClothesExtended(Clothes clothes)
        {
            this.Id = clothes.Id;
            this.Code = clothes.Code;
            this.Label = clothes.Label;
        }
    }
}
