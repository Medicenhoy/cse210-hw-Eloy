using System;
using System.Collections.Generic;
using System.IO;

// Base class for all goal types
abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }
    private bool _isCompleted;

    public void SetCompletionStatus(bool status)
    {
        _isCompleted = status;
    }

    public bool IsCompleted => _isCompleted;

    public Goal(string name, int points)
    {
        Name = name;
        Points = points;
        _isCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveData();
}

// Simple goals can be completed once
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
    
    public override string SaveData()
    {
        return $"SimpleGoal|{Name}|{Points}|{IsCompleted}";
    }
}

// Eternal goals never get marked as completed, but always give points
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
    
    public override string SaveData()
    {
        return $"EternalGoal|{Name}|{Points}";
    }
}

// Checklist goals must be repeated a number of times to be completed
class ChecklistGoal : Goal
{
    public int TargetCount { get; }
    protected int _currentCount;
    public int Bonus { get; }

    public int CurrentCount => _currentCount;
    public void SetCurrentCount(int count) { _currentCount = count; }

    public ChecklistGoal(string name, int points, int targetCount, int bonus) : base(name, points)
    {
        TargetCount = targetCount;
        _currentCount = 0;
        Bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            _currentCount++;
            if (_currentCount >= TargetCount)
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
        return (IsCompleted ? "[X] " : "[ ] ") + Name + $" (Completed {_currentCount}/{TargetCount})";
    }
    
    public override string SaveData()
    {
        return $"ChecklistGoal|{Name}|{Points}|{_currentCount}|{TargetCount}|{Bonus}";
    }
}

// Manages the goals and user score
class QuestManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            _score += _goals[index].RecordEvent();
        }
    }

    public void ShowGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatus()}");
        }
        Console.WriteLine($"Total Score: {_score}");
    }
    
    public void SaveProgress()
    {
        using (StreamWriter writer = new StreamWriter("progress.txt"))
        {
            writer.WriteLine(_score);
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.SaveData());
            }
        }
    }
    
    public void LoadProgress()
    {
        if (File.Exists("progress.txt"))
        {
            string[] lines = File.ReadAllLines("progress.txt");
            _score = int.Parse(lines[0]);
            _goals.Clear();
            
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                switch (parts[0])
                {
                    case "SimpleGoal":
                        SimpleGoal simpleGoal = new SimpleGoal(parts[1], int.Parse(parts[2]));
                        simpleGoal.SetCompletionStatus(bool.Parse(parts[3]));
                        _goals.Add(simpleGoal);
                        break;
                    case "EternalGoal":
                        _goals.Add(new EternalGoal(parts[1], int.Parse(parts[2])));
                        break;
                    case "ChecklistGoal":
                        ChecklistGoal checklistGoal = new ChecklistGoal(parts[1], int.Parse(parts[2]), int.Parse(parts[4]), int.Parse(parts[5]));
                        checklistGoal.SetCompletionStatus(int.Parse(parts[3]) >= int.Parse(parts[4]));
                        checklistGoal.SetCurrentCount(int.Parse(parts[3]));
                        _goals.Add(checklistGoal);
                        break;
                }
            }
        }
    }
}

// Main program to interact with the user
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
            Console.WriteLine("4. Save Progress");
            Console.WriteLine("5. Load Progress");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());
                    Console.Write("Choose goal type (1: Simple, 2: Eternal, 3: Checklist): ");
                    int type = int.Parse(Console.ReadLine());
                    if (type == 1)
                        manager.AddGoal(new SimpleGoal(name, points));
                    else if (type == 2)
                        manager.AddGoal(new EternalGoal(name, points));
                    else
                        manager.AddGoal(new ChecklistGoal(name, points, 5, 50));
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
                    manager.SaveProgress();
                    break;
                case 5:
                    manager.LoadProgress();
                    break;
                case 6:
                    running = false;
                    break;
            }
        }
    }
}
