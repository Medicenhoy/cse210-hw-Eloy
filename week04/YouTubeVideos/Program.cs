using System;
using System.Collections.Generic;

// Eloy Francisco Martinez Cabello
// W04 Assignment: YouTube Video Program

// Class representing a comment on a YouTube video

class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Class representing a YouTube video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.Name}: {comment.Text}");
        }
        Console.WriteLine(new string('-', 40));
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        // Creating videos
        Video video1 = new Video("C# Basics", "John Doe", 600);
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Can you make a video on LINQ?"));

        Video video2 = new Video("Advanced C#", "Jane Smith", 1200);
        video2.AddComment(new Comment("Dave", "Really in-depth, loved it!"));
        video2.AddComment(new Comment("Eva", "Awesome content, keep it up!"));
        video2.AddComment(new Comment("Frank", "Could you cover async programming next?"));

        Video video3 = new Video("C# Design Patterns", "Emily Johnson", 900);
        video3.AddComment(new Comment("Grace", "Super useful, thanks!"));
        video3.AddComment(new Comment("Hank", "Loved the Singleton pattern explanation!"));
        video3.AddComment(new Comment("Ivy", "Looking forward to more videos."));

        // Adding videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Displaying video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}