﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class SizeExtensions
    {
        public static void ApplyChange(this Size dbSize, Size size)
        {
            if (!String.IsNullOrWhiteSpace(size.Label))
            {
                dbSize.Label = size.Label;
            }
            if (!String.IsNullOrWhiteSpace(size.Code))
            {
                dbSize.Code = size.Code;
            }
        }
    }
}
