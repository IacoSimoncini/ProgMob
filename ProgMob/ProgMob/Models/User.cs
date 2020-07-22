using System;
using System.Collections.Generic;
using System.Text;

namespace ProgMob.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Id { get; set; }
        public string Uri { get; set; }

        public User(string Name, string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Id = " ";
            this.Uri = " ";
        }
    }
}
