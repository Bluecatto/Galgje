using System;

namespace Galgje
{
    class Program
    {
        //Maak galgje, gebaseerd op een interne lijst met woorden.
        static List<string> wordList = new List<string> { "school", "hangman", "fortnite", "xylofoon", "lijst" };

        //1 speler, 10 kansen.
        static string playerName;
        static int attempts = 10;
        static string guess;
        static string chosenWord;
        static bool gameWon;
        static List<HangmanLetter> guessedLetters = new List<HangmanLetter>();

        public struct HangmanLetter
        {
            public char letter;
            public bool isInWord;
        }
        //Letter raden, niet in het woord, dan kans eraf, wel in het woord, dan letter tonen.
        //Alle letters geraden, dan gewonnen.
        //Kansen op, dan game over.

        static void Main(string[] args)
        {
            Random rnd = new Random();
            chosenWord = wordList[rnd.Next(0, wordList.Count)];

            //Speler naam vragen
            Console.WriteLine("Hallo speler, wat is je naam?");
            playerName = Console.ReadLine();
            //Speler welkom heten
            //Spel spelen

            PlayGame();
        }

        static void PlayGame()
        {
            //Zo lang het woord niet geraden is of de attemps niet 0 zijn
            //Voer een raad actie uit
            while (attempts > 0 && gameWon == false)
            {
                Guess();
            }
        }

        static void Guess()
        {
            DisplayGame();
            Console.WriteLine("Raad een letter if het gehele woord.");

            //Letter invoer uitlezen
            guess = Console.ReadLine().ToLower();

            //Controleren of input valide is (alleen letters)
            foreach (char c in guess)
            {
                if (!char.IsLetter(c))
                {
                    //Geen valide input
                    Console.WriteLine("Je hebt andere tekens dan letters in je invoer. ");
                    //Opnieuw proberen
                    return;
                }
            }
            //Controleren of het een letter of een woord is

            if (guess.Length == 1)
            {
                //Is een letter
                //Check of letter al eerder geraden
                foreach (HangmanLetter h in guessedLetters)
                {
                    if (h.letter == guess[0])
                    {
                        //Letter al eerder geraden
                        return;
                    }
                }
                //Check of letter in woord zit
                //Controleren of letter in woord zit
                //Letter opslaan als struct
                if (LetterInWoord(guess[0], chosenWord))
                {
                    guessedLetters.Add(new HangmanLetter() { letter = guess[0], isInWord = true });
                }
                else
                {
                    guessedLetters.Add(new HangmanLetter() { letter = guess[0], isInWord = false });
                    attempts--;
                }

            }

            else if (guess.Length > 1)
            {
                if (string.Compare(guess, chosenWord) == 0)
                {
                    gameWon = true;
                    return;
                }
                //Is een woord
            }
            else
            {
                //Geen valide poging
                Console.WriteLine("Geen correcte invoer. Probeer opnieuw.");
                return;
            }
            //Controleren of woord klaar is
            if (WordComplete())
            {
                gameWon = true;
                return;
            }
            //Geraden letters tonen
        }

        static void DisplayGame()
        {
            Console.WriteLine("Hallo " + playerName);
            Console.WriteLine("Je hebt nog " + attempts + " pogingen");
            foreach (char c in chosenWord)
            {
                char disPlayLetter = '_';
                foreach(HangmanLetter h in guessedLetters)
                {
                    if (h.letter == c)
                    {
                        disPlayLetter = h.letter;
                    }
                }
               Console.Write(disPlayLetter);
            }

            Console.WriteLine();
            Console.WriteLine("Guessed letters:");
            foreach(HangmanLetter h in guessedLetters)
            {
                if ( h.isInWord == false)
                {
                    Console.Write(h.letter + " ");
                }
            }
        }

        static bool WordComplete()
        {
            int uniqueLetter = chosenWord.Distinct().Count();
            foreach(HangmanLetter h in guessedLetters)
            {
                if (h.isInWord)
                {
                    uniqueLetter--;
                }
            }
            if (uniqueLetter == 0)
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        static bool LetterInWoord(char letter, string word)
        {
            foreach(char c in word)
            {
                if(c == letter)
                {
                    return true;
                }
            }
            return false;
        }
    }
}