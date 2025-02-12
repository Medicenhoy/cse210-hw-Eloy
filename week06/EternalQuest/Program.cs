using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }
    public bool IsCompleted { get; protected set; }

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        IsCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();

    protected void SetCompletionStatus(bool status)
    {
        IsCompleted = status;
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name, points) {}

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            SetCompletionStatus(true);
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return IsCompleted ? "[X] " + Name : "[ ] " + Name;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) {}

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetStatus()
    {
        return "[∞] " + Name;
    }
}

class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Bonus { get; set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonus) : base(name, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        Bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            CurrentCount++;
            if (CurrentCount >= TargetCount)
            {
                SetCompletionStatus(true);
                return Points + Bonus;
            }
            return Points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        return (IsCompleted ? "[X] " : "[ ] ") + Name + $" (Completed {CurrentCount}/{TargetCount})";
    }
}

class QuestManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            score += goals[index].RecordEvent();
        }
    }

    public void ShowGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
        }
        Console.WriteLine($"Total Score: {score}");
    }
}

class Program
{
    static void Main()
    {
        QuestManager manager = new QuestManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());
                    manager.AddGoal(new SimpleGoal(name, points));
                    break;
                case 2:
                    manager.ShowGoals();
                    Console.Write("Enter goal number to record event: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordGoalEvent(index);
                    break;
                case 3:
                    manager.ShowGoals();
                    break;
                case 4:
                    running = false;
                    break;
            }
        }
    }
}
