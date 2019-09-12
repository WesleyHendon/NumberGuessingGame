using System;

namespace NumberGuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Let's play a number guessing game. The higher your score, the worse you did!");
            int maxValue = GetUserInput("What should be the maximum number in the random range?");
            int randomValue = new Random().Next(1, maxValue + 1);
            int guessedNumber = -1;
            do
            {
                if (guessedNumber != -1)
                {
                    string incorrectMessage = "Incorrect!";
                    if (guessedNumber > randomValue)
                    {
                        incorrectMessage += " Try going lower...";
                    }
                    else
                    {
                        incorrectMessage += " Try going higher...";
                    }
                    System.Console.WriteLine(incorrectMessage);
                }
                guessedNumber = GetUserInput("Guess a number: ");
            }
            while (guessedNumber != randomValue);

            System.Console.WriteLine("Correct!");
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
    }
}
