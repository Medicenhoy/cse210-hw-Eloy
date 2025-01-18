using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store the numbers entered by the user
        List<int> numbers = new List<int>();
        
        // Initialize userNumber to -1 to start the loop
        int userNumber = -1;

        // Loop until the user enters 0
        while (userNumber != 0)
        {
            Console.Write("Enter a number (0 to quit): ");
            
            // Read user input and parse it as an integer
            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);
            
            // Only add the number to the list if it is not 0
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }

        // Part 1: Compute the sum of the numbers
        int sum = 0;
        foreach (int number in numbers)
        {
            // Add each number to the sum
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");

        // Part 2: Compute the average of the numbers
        // Cast the sum to float to avoid integer division
        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Part 3: Find the maximum value in the list
        // Start with the first number as the initial max
        int max = numbers[0];

        foreach (int number in numbers)
        {
            // If this number is greater than the current max, update the max
            if (number > max)
            {
                max = number;
            }
        }

        // Display the maximum value
        Console.WriteLine($"The max is: {max}");
    }
}
