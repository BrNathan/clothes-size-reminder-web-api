using Entities.Models;
using System;

namespace Entities.Extensions
{
    public static class UserExtensions
    {
        public static void ApplyChange(this User dbUser, User user)
        {
            if (!String.IsNullOrWhiteSpace(user.Email))
            {
                dbUser.Email = user.Email;
            }
            if (!String.IsNullOrWhiteSpace(user.FirstName))
            {
                dbUser.FirstName = user.FirstName;
            }
            if (!String.IsNullOrWhiteSpace(user.LastName))
            {
                dbUser.LastName = user.LastName;
            }
        }
    }
}
