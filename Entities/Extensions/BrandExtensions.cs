using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class BrandExtensions
    {
        public static void ApplyChange(this Brand dbBrand, Brand brand)
        {
            if (!String.IsNullOrWhiteSpace(brand.Name))
            {
                dbBrand.Name = brand.Name;
            }
            if (!String.IsNullOrWhiteSpace(brand.CorporateName))
            {
                dbBrand.CorporateName = brand.CorporateName;
            }
        }
    }
}
