using System;

// Base class Assignment
public class Assignment
{
    private string _studentName;
    private string _topic;

    // Constructor
    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    // Method to get the summary
    public string GetSummary()
    {
        return $"{_studentName} - {_topic}";
    }

    // Getter for student name
    public string GetStudentName()
    {
        return _studentName;
    }
}

// Derived class MathAssignment
public class MathAssignment : Assignment
{
    private string _textbookSection;
    private string _problems;

    // Constructor (calls the base class constructor)
    public MathAssignment(string studentName, string topic, string textbookSection, string problems) 
        : base(studentName, topic)
    {
        _textbookSection = textbookSection;
        _problems = problems;
    }

    // Method to get the list of exercises
    public string GetHomeworkList()
    {
        return $"Section {_textbookSection} Problems {_problems}";
    }
}

// Derived class WritingAssignment
public class WritingAssignment : Assignment
{
    private string _title;

    // Constructor (calls the base class constructor)
    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        _title = title;
    }

    // Method to get writing information
    public string GetWritingInformation()
    {
        return $"{_title} by {GetStudentName()}";
    }
}

// Main class with the Main method
class Program
{
    static void Main()
    {
        // Test the base class Assignment
        Assignment assignment = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(assignment.GetSummary());

        // Test the MathAssignment class
        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // Test the WritingAssignment class
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}
