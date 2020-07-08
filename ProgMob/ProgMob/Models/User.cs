using System;
using System.Collections.Generic;
using System.Text;

namespace ProgMob.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string Username , string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
