using System;

class Program
{
    static void Main(string[] args)
    {
        // Call method to display a welcome message
        DisplayWelcomeMessage();

        // Prompt the user for their name and store it
        string userName = PromptUserName();

        // Prompt the user for their favorite number and store it
        int userNumber = PromptUserNumber();

        // Call the method to square the user's number
        int squaredNumber = SquareNumber(userNumber);

        // Display the result to the user
        DisplayResult(userName, squaredNumber);
    }

    // Method to display a welcome message
    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Method to ask the user for their name and return it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();

        return name;
    }

    // Method to ask the user for their favorite number and return it
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());

        return number;
    }

    // Method to square a given number
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // Method to display the result, showing the user's name and the squared number
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}
