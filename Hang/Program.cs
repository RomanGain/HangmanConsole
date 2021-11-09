using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hang.Models;


namespace Hang
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first player name");
            string player1Name = Console.ReadLine();
            PlayerType player1Type = PlayerType.GuessWordPlayer;

            Console.WriteLine("Enter second player name");
            string player2Name = Console.ReadLine();
            PlayerType player2Type = PlayerType.MakeWordPlayer;

            Console.WriteLine("Enter word:");
            string guessedWord = Console.ReadLine();


            Player player1 = new Player(player1Name, player1Type);
            Player player2 = new Player(player2Name, player2Type);
            List<Player> players = new List<Player>() { player1, player2 };

            Word word = new Word(guessedWord);
            Game game = new Game(players, word);
            game.Start();
        }
    }
}
