using System;
using System.Collections.Generic;
using System.Text;

namespace ProgMob.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Uri { get; set; }
        public string Id { 
            get { return Id; }
            set { Id = value; } 
        }

        public User(string Name, string Surname, string Id)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Id = Id;
        }
    }
}
