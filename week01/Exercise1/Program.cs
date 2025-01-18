using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask the user for their first name
        Console.Write("¿Cuál es tu primer nombre? ");
        string primerNombre = Console.ReadLine();

        // Ask the user for their last name
        Console.Write("¿Cuál es tu apellido? ");
        string apellido = Console.ReadLine();

        // Display the full name in a specific format
        Console.WriteLine($"Tu nombre es {apellido}, {primerNombre} {apellido}.");
    }
}

