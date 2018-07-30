namespace DatabaseMethods
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }

        public Game(int id, string name, Genre genre)
        {
            Id = id;
            Name = name;
            Genre = genre;
        }

        public Game()
        {
        }

        public override string ToString()
        {
            return $"Name: {Name}, Genre: {Genre.ToString()}";
        }
    }
}
