using System;

class Program
{
    static void Main(string[] args)
    {
        // Testing default constructor
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString()); // Output: 1/1
        Console.WriteLine(f1.GetDecimalValue());   // Output: 1

        // Testing constructor with one parameter
        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString()); // Output: 5/1
        Console.WriteLine(f2.GetDecimalValue());   // Output: 5

        // Testing constructor with two parameters
        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString()); // Output: 3/4
        Console.WriteLine(f3.GetDecimalValue());   // Output: 0.75

        // Testing another fraction
        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString()); // Output: 1/3
        Console.WriteLine(f4.GetDecimalValue());   // Output: 0.3333333333333333
    }
}
