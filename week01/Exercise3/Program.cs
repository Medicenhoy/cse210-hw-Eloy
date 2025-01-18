using System;

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of Random to generate a random number
        Random randomGenerator = new Random();

        // Generate a random magic number between 1 and 100
        int magicNumber = randomGenerator.Next(1, 101);

        // Initialize guess with a value that is unlikely to match the magic number
        int guess = -1;

        // Loop until the user guesses the correct number
        while (guess != magicNumber)
        {
            // Prompt the user to enter their guess
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            // If the magic number is greater than the guess, ask for a higher guess
            if (magicNumber > guess)
            {
                Console.WriteLine("Higher");
            }
            // If the magic number is less than the guess, ask for a lower guess
            else if (magicNumber < guess)
            {
                Console.WriteLine("Lower");
            }
            // If the guess is correct, congratulate the user
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}
