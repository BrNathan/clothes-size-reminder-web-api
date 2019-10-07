using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class UserRoleExtensions
    {
        public static void ApplyChange(this UserRole dbUserRole, UserRole userRole)
        {
            //if (!String.IsNullOrWhiteSpace(userRole..Code))
            //{
            //    dbUserRole.Code = userRole.Code;
            //}
            //if (!String.IsNullOrWhiteSpace(userRole.Label))
            //{
            //    dbUserRole.Label = userRole.Label;
            //}
        }
    }
}
