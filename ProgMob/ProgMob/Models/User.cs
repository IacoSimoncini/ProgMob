using System;
using Xamarin.Forms;

namespace ProgMob.Models
{
    public class User
    {
        private string name;
        private string surname;
        private string username;
        private string email;
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Id { get; set; }
        public string Uri { get; set; }
        
        public User(string name, string surname, string uri, string username, string email)
        {
            this.Name = name;
            this.Surname = surname;
            this.Id = " ";
            this.Uri = uri;
            this.Username = username;
            this.Email = email;
        }
    }
}
