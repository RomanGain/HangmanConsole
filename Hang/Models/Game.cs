using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hang.Models
{
    public delegate void GameEndEventHandler();
    public delegate void GameNotificationEventHandler();

    public class Game
    {
        public event GameEndEventHandler GameWin;
        public event GameEndEventHandler GameLoose;
        public event GameEndEventHandler GameUnclear;
        public event GameNotificationEventHandler GameShowWord;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public List<Player> players;
        public Word word;

        List<string> usedLetters; // the letters player 2 already named
        List<string> leftLettersInWord; //letters that exist in word but were not named yet

        string[] finalWord; //word to show at console at the end.


        bool isWin;
        bool isLoose;


        int failsCount;
        readonly int maxFails = 7;

        public Game(List<Player> players, Word word)
        {
            this.players = players;
            this.word = word;

            finalWord = new string[word.Length];

            usedLetters = new List<string>();
            leftLettersInWord = new List<string>();
        }


        public void MakeGuessAttempt(string letter)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(letter))
                {
                    if (!IsLetterAlreadyBeenGuessed(letter))
                    {
                        if (IsLetterExistInWord(letter))
                        {
                            DeleteLetterFromLeftLetters(letter);
                            string shownWord = GetShownWord();
                            Console.WriteLine(shownWord);
                        }
                        else
                        {
                            ++failsCount;
                            Console.WriteLine($"Letter {letter} NOT exist in word");
                            Console.WriteLine($"{maxFails - failsCount} attempts left");
                        }
                        usedLetters.Add(letter);
                    }
                    else
                    {
                        Console.WriteLine($"Letter {letter} already been guessed");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// Check if letter already been tried to guess.
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        private bool IsLetterAlreadyBeenGuessed(string letter)
        {
            if (usedLetters != null && usedLetters.Count > 0)
            {
                if (usedLetters.Contains(letter))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private bool IsLetterExistInWord(string letter)
        {
            if (leftLettersInWord.Contains(letter))
                return true;
            else
                return false;
        }
        private void DeleteLetterFromLeftLetters(string letter)
        {
            int letterIndexInWord = GetLetterIndexInWord(letter);
            finalWord[letterIndexInWord] = letter;

            leftLettersInWord.Remove(letter);
            Console.WriteLine($"Letter {letter} exist in word");
        }

        private int GetLetterIndexInWord(string letter)
        {
            int letterIndexInWord = word.Value.IndexOf(letter);
            return letterIndexInWord;
        }



        public void SetWordLettersToList(string guessedWord)
        {
            try
            {
                leftLettersInWord = new List<string>();

                for (int i = 0; i <= guessedWord.Length - 1; i++)
                {
                    leftLettersInWord.Add(guessedWord[i].ToString().ToLower());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }




        private bool IsWin()
        {
            if (leftLettersInWord.Count > 0)
                return false;
            else
                return true;
        }


        private bool IsLoose()
        {
            if (failsCount < maxFails)
                return false;
            else
                return true;
        }



        private string GetLetter()
        {
            try
            {
                Console.WriteLine("Insert letter:");

                string currentLetter = Console.ReadLine().ToLower();
                return currentLetter;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }


        public void Start()
        {
            try
            {
                failsCount = 0;
                SetWordLettersToList(word.Value);

                string wordWithStars = GetShownWord();
                Console.WriteLine(wordWithStars);

                string currentLetter;

                while (!isWin && !isLoose)
                {
                    currentLetter = GetLetter();
                    MakeGuessAttempt(currentLetter);


                    isWin = IsWin();
                    isLoose = IsLoose();
                }

                Stop();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        public void Stop()
        {
            if (isWin)
            {
                GameWin?.Invoke();
                Console.WriteLine("You Win!");
            }
            else if (isLoose)
            {
                GameLoose?.Invoke();
                Console.WriteLine("You Loose. Game Over.");
            }
            else
            {
                GameUnclear?.Invoke();
                Console.WriteLine("Game Over");
            }

            Console.ReadLine();
        }

        private string GetShownWord()
        {
            string shownWord = null;

            for (int i = 0; i < word.Length; i++)
            {
                if (finalWord[i] == null)
                    shownWord += "*";
                else
                    shownWord += finalWord[i];
            }
            return shownWord;
        }
    }
}