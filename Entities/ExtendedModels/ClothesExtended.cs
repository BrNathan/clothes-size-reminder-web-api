using Entities.Models;
using System;

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
            if (clothes.Id.HasValue)
            {
                this.Id = clothes.Id.Value;
                this.Code = clothes.Code;
                this.Label = clothes.Label;
            }
            else
            {
                throw new Exception("Cannot create ClothesExtended - the clothes id is null");
            }
        }
    }
}
