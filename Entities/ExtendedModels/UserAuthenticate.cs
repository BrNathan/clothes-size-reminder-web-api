using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ExtendedModels
{
    public class UserAuthenticate: User
    {
        public string Token { get; set; }

        public new string Password = null;

        public UserAuthenticate(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.GenderId = user.GenderId;

            this.Password = null;
            this.Token = null;
        }

        public UserAuthenticate(User user, string token): this(user)
        {
            this.Token = token;
        }
    }
}
