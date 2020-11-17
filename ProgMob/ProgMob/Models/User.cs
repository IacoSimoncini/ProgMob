using System;
using Xamarin.Forms;

namespace ProgMob.Models
{
    public class User
    {
        private string name;
        private string surname;
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Id { get; set; }
        public string Uri { get; set; }
        public ImageSource Source { get; set; }

        public User(string Name, string Surname, string uri)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Id = " ";
            this.Uri = uri;
            this.Source = ImageSource.FromUri(new Uri(this.Uri));
        }
    }
}
