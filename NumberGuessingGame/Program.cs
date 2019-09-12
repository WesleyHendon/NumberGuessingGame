using System;
using System.Collections.Generic;

namespace NumberGuessingGame
{
    class Program
    {
        static GuessedNumber currentGuess_aboveRandom = null;
        static GuessedNumber currentGuess_belowRandom = null;
        static void Main(string[] args)
        {
            System.Console.WriteLine("Let's play a number guessing game. The higher your score, the worse you did!");
            int maxValue = GetUserInput("What should be the maximum number in the random range?");
            int randomValue = new Random().Next(1, maxValue + 1);
            int guessedNumber = -1;
            int numberOfGuesses = 0;
            do
            {
                Console.Beep();
                Console.Clear();
                System.Console.WriteLine(" -- You are guessing in the range 1-" + maxValue + " --");
                //string tipGiven = ""; // shorthand, for displaying
                //string incorrectMessage = "Incorrectly!";
                /*if (numberOfGuesses > 0)
                {
                    if (guessedNumber > randomValue)
                    {
                        incorrectMessage += " Try going lower...";
                        tipGiven = "(low)";
                    }
                    else
                    {
                        incorrectMessage += " Try going higher...";
                        tipGiven = "(high)";
                    }
                    //System.Console.WriteLine(incorrectMessage);
                }*/
                if (numberOfGuesses > 0)
                {
                    //System.Console.WriteLine("You have guessed: " + incorrectMessage);
                    //System.Console.WriteLine();
                    //PrintPreviousGuesses(allGuesses);
                }
                string result = "";
                if (currentGuess_belowRandom != null)
                {
                    result += "Number is conta\nined between: " + currentGuess_belowRandom.ToString() + " => { } <= ";
                }
                if (currentGuess_aboveRandom != null)
                {
                    result += currentGuess_aboveRandom;
                }
                System.Console.WriteLine(result == "" ? "No guesses yet" : result);
                guessedNumber = GetUserInput("Guess a number: ");
                numberOfGuesses++;
                NewGuess(new GuessedNumber(guessedNumber, randomValue));
            }
            while (guessedNumber != randomValue);
            Console.Clear();
            System.Console.WriteLine($"Correct! It took you {numberOfGuesses} guesses!");
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
            //return (IsLower()) ? actual + " is below" : actual + " is above";
        }
    }
}
