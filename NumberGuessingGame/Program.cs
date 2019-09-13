using System;
using System.Collections.Generic;

namespace NumberGuessingGame
{
    class Program
    {
        static GuessedNumber currentGuess_aboveRandom;
        static GuessedNumber currentGuess_belowRandom;
        static void Main(string[] args)
        {
            currentGuess_aboveRandom = null;
            currentGuess_belowRandom = null;
            string firstUserInput = GetUserInput("Would you like to play a game? (y/n/m)", 1);
            bool userWantsToPlay = false;
            switch (firstUserInput)
            {
                case "y":
                    userWantsToPlay = true;
                    break;
                case "n":
                    System.Console.WriteLine("Why are you even here?");
                    return;
                case "m":
                    System.Console.WriteLine("You wont get anywhere in life with that amount of indecisiveness...");
                    Main(new string[] { "" });
                    return;
                default:
                    Main(new string[] { "" });
                    return;
            }
            if (userWantsToPlay)
            {
                System.Console.WriteLine("Let's play a number guessing game. The higher your score, the worse you did!");
                int minValue = GetUserInput("What should the low end of the range be?");
                int maxValue = GetUserInput("What should the maximum number in the range be?");
                int maxGuesses = GetUserInput("How many guesses should be allowed? (put 0 for infinite guesses)");
                int randomValue = new Random().Next(minValue, maxValue + 1);
                int guessedNumber = -1;
                int numberOfGuesses = 0;
                bool exceededGuessLimit = false;
                do
                {
                    Console.Beep();
                    Console.Clear();
                    System.Console.WriteLine(" -- You are guessing in the range " + minValue + "-" + maxValue + " --");
                    if (maxGuesses != 0)
                    {
                        System.Console.WriteLine("You have used " + numberOfGuesses + "/" + maxGuesses + " guesses");
                    }
                    else
                    {
                        System.Console.WriteLine("You have guessed " + numberOfGuesses + " times");
                    }
                    string result = "";
                    result += "Number is contained between: " + (currentGuess_belowRandom != null ? currentGuess_belowRandom.ToString() : minValue.ToString()) + " => { ?? } <= ";
                    result += (currentGuess_aboveRandom != null ? currentGuess_aboveRandom.ToString() : maxValue.ToString());
                    System.Console.WriteLine(numberOfGuesses == 0 ? "No guesses yet" : result);
                    guessedNumber = GetUserInput("Guess a number: ");
                    numberOfGuesses++;
                    NewGuess(new GuessedNumber(guessedNumber, randomValue));
                    if (maxGuesses != 0 && numberOfGuesses >= maxGuesses)
                    {
                        exceededGuessLimit = true;
                    }
                }
                while (guessedNumber != randomValue && !exceededGuessLimit);
                Console.Clear();
                if (maxGuesses != 0 && !exceededGuessLimit)
                {
                    System.Console.WriteLine($"Correct! It took you {numberOfGuesses}/{maxGuesses} guesses to guess "
                        + randomValue
                        + " in the range of "
                        + minValue + "-" + maxValue);
                }
                else if (maxGuesses != 0 && exceededGuessLimit)
                {
                    System.Console.WriteLine($"You have failed miserably to guess {randomValue} on the range {minValue}/{maxValue} with {maxGuesses} amount of tries.");
                    string response = GetUserInput("Would you like to try again? (y/n)", 1);
                    if (response == "y")
                    {
                        Main(new string[] { "" });
                        return;
                    }
                    else
                    {
                        System.Console.WriteLine("I guess you like being a failure...");
                    }
                }
                else
                {
                    System.Console.WriteLine($"Correct! It took you {numberOfGuesses} guesses to guess "
                        + randomValue
                        + " in the range of "
                        + minValue + "-" + maxValue);
                }
            }
        }

        static int GetUserInput(string question)
        {
            int integerFromUser;
            bool success;
            do
            {
                Console.WriteLine(question);
                success = int.TryParse(Console.ReadLine(), out integerFromUser);
            }
            while (!success);
            return integerFromUser;
        }

        static string GetUserInput(string question, int desiredInputLength)
        {
            string userResponse = "";
            bool success;
            do
            {
                Console.WriteLine(question);
                userResponse = Console.ReadLine();
                if (userResponse.Length == desiredInputLength)
                {
                    success = true;
                }
                else
                {
                    Console.WriteLine($"Expected a {desiredInputLength} character long response");
                    success = false;
                }
            }
            while (!success);
            return userResponse;
        }

        static void NewGuess(GuessedNumber guessedNumber)
        {
            if (guessedNumber.IsLower() && currentGuess_belowRandom == null)
            {
                currentGuess_belowRandom = guessedNumber;
            }
            else if (!guessedNumber.IsLower() && currentGuess_aboveRandom == null)
            {
                currentGuess_aboveRandom = guessedNumber;
            }
            
            if (currentGuess_belowRandom != null && guessedNumber.IsHigherThan(currentGuess_belowRandom) && guessedNumber.IsLower())
            {
                currentGuess_belowRandom = guessedNumber;
            }
            else if (currentGuess_aboveRandom != null && !guessedNumber.IsHigherThan(currentGuess_aboveRandom) && !guessedNumber.IsLower())
            {
                currentGuess_aboveRandom = guessedNumber;
            }
        }
    }

    class GuessedNumber
    {
        int actual;
        int expected; // this is the number they need to guess
        public GuessedNumber(int actual_, int expected_)
        {
            actual = actual_;
            expected = expected_;
        }

        public bool IsHigherThan(GuessedNumber number)
        {
            if (actual > number.actual)
            {
                return true;
            }
            return false;
        }

        public bool IsLower()
        {
            if (actual < expected)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return actual.ToString();
        }
    }
}