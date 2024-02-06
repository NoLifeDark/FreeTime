using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Guess the Number game!");

        // Generoi satunnaisen numeron väliltä 1-100
        Random random = new Random();
        int secretNumber = random.Next(1, 101);

        int attempts = 0;
        bool hasGuessedCorrectly = false;

        while (!hasGuessedCorrectly)
        {
            Console.Write("Enter your guess (1-100): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int userGuess))
            {
                attempts++;

                if (userGuess == secretNumber)
                {
                    hasGuessedCorrectly = true;
                }
                else
                {
                    // Vinkki oikeaan numeroon
                    string hint = (userGuess < secretNumber) ? "Too low" : "Too high";
                    Console.WriteLine($"{hint}. Try again!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        Console.WriteLine($"Congratulations! You guessed the number in {attempts} attempts. The secret number was {secretNumber}.");

        Console.ReadLine();
    }
}