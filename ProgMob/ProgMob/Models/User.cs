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
        public string ID { 
            get { return ID; }
            set { ID = value; } 
        }

        public User(string Name, string Surname, string ID)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.ID = ID;
        }
    }
}
