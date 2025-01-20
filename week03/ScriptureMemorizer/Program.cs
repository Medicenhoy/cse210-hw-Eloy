using System;
using System.Collections.Generic;
using System.IO;

// Eloy Francisco Martinez Cabello
// Well, this program allows to work with a library of scripts that are randomly selected and load scripts from a file that I add to the list.
// W03 Team Activity: Scripture Memorizer Design

class Program
{
    static void Main(string[] args)
    {
        ScriptureLibrary library = new ScriptureLibrary("scriptures.txt"); // Loading scripts from a file

        foreach (Scripture scripture in library.GetAllScriptures())
        {
            while (!scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetFormattedScripture());
                Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");

                string input = Console.ReadLine();
                if (input.ToLower() == "quit") return;

                scripture.HideRandomWords(3); // Hide up to 3 words per iteration
            }

            Console.Clear();

            Console.WriteLine("All words hidden for this scripture! Moving to the next one...\n");
            Console.WriteLine(scripture.GetFormattedScripture());
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        Console.Clear();
        Console.WriteLine("All scriptures completed. Well done!");
    }
}

class ScriptureLibrary
{
    private List<Scripture> _scriptures;

    public ScriptureLibrary(string filePath)
    {
        _scriptures = new List<Scripture>();
        LoadScripturesFromFile(filePath);
    }

    private void LoadScripturesFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|'); // Format: “Reference|Text”.
                if (parts.Length == 2)
                {
                    Reference reference = new Reference(parts[0]);
                    _scriptures.Add(new Scripture(reference, parts[1]));
                }
            }
        }
        else
        {
            Console.WriteLine("Scripture file not found. Using default scripture.");
            _scriptures.Add(new Scripture(new Reference("John 3:16"), "For God so loved the world that he gave his one and only Son."));
        }
    }

    public Scripture GetRandomScripture()
    {
        Random random = new Random();
        return _scriptures[random.Next(_scriptures.Count)];
    }

    public List<Scripture> GetAllScriptures()
    {
        return _scriptures;
    }
}

class Reference
{
    public string FullReference { get; private set; }

    public Reference(string reference)
    {
        FullReference = reference;
    }

    public override string ToString()
    {
        return FullReference;
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        int hidden = 0;

        while (hidden < count && _words.Exists(w => !w.IsHidden))
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden)
            {
                _words[index].Hide();
                hidden++;
            }
        }
    }

    public bool AllWordsHidden()
    {
        return _words.TrueForAll(w => w.IsHidden);
    }

    public string GetFormattedScripture()
    {
        string scriptureText = string.Join(" ", _words);
        return $"{_reference}\n{scriptureText}";
    }
}