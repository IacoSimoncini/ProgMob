namespace ProgMob.Models
{
    public class User
    {
        private string name;
        private string surname;
        //private string id;
        //private string uri;
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
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
