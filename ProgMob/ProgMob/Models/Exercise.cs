namespace ProgMob.Models
{
    public class Exercise
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string Id { get; set; }
        public bool IsChecked { get; set; }

        public Exercise(string Name, string Description, string Difficulty)
        {
            this.Name = Name;
            this.Description = Description;
            this.Difficulty = Difficulty;
            this.Id = " ";
            IsChecked = false;
        }

    }
}
