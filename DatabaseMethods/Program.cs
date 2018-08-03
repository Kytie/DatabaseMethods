using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DatabaseMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vanilla");
            GetGamesVanilla();
            Console.WriteLine("\nEntity Framework");
            GetGamesEntityFrameWork();
        }

        private static void GetGamesEntityFrameWork()
        {
            var db = new GamesDb();
            var games = db.Games;
            var genres = db.Genres;

            var query = from game in games
                        join genre in genres on game.Genre.Id equals genre.Id
                        select new
                        {
                            Id = game.Id,
                            Name = game.Name,
                            Genre = genre
                        };

            var query2 = games.Join(
                            genres,
                            game => game.Genre.Id,
                            genre => genre.Id,
                            (game, genre) => new
                            {
                                Id = game.Id,
                                Name = game.Name,
                                Genre = genre
                            });

            Console.WriteLine("\nQuery Syntax");
            foreach (var game in query)
            {
                Game newGame = new Game(game.Id, game.Name, game.Genre);
                Console.WriteLine(newGame.ToString());
            }

            Console.WriteLine("\nMethod Syntax");
            foreach (var game in query2)
            {
                Game newGame = new Game(game.Id, game.Name, game.Genre);
                Console.WriteLine(newGame.ToString());
            }
        }

        private static void GetGamesVanilla()
        {
            List<Game> GameList = new List<Game>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDatabase"].ConnectionString))
            {
                var query = "SELECT * FROM Games JOIN Genres ON Games.Genre_Id=Genres.Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Game newGame = new Game();
                        Genre newGenre = new Genre();

                        newGenre.Id = reader.GetInt32(3);
                        newGenre.Name = reader.GetString(4);

                        newGame.Id = reader.GetInt32(0);
                        newGame.Name = reader.GetString(1);
                        newGame.Genre = newGenre;

                        GameList.Add(newGame);
                    }
                }
            }

            foreach(var game in GameList)
            {
                Console.WriteLine(game.ToString());
            }
        }
    }
}
