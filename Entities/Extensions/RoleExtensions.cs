using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class RoleExtensions
    {
        public static void ApplyChange(this Role dbRole, Role role)
        {
            if (!String.IsNullOrWhiteSpace(role.Code))
            {
                dbRole.Code = role.Code;
            }
            if (!String.IsNullOrWhiteSpace(role.Label))
            {
                dbRole.Label = role.Label;
            }
        }
    }
}
