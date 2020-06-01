using Entities.Models;
using System;

namespace Entities.Extensions
{
    public static class GenderExtensions
    {
        public static void ApplyChange(this Gender dbGender, Gender gender)
        {
            if (!String.IsNullOrWhiteSpace(gender.Code))
            {
                dbGender.Code = gender.Code;
            }
            if (!String.IsNullOrWhiteSpace(gender.Label))
            {
                dbGender.Label = gender.Label;
            }
        }
    }
}
