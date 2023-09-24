using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<string> prompts = new List<string>
    {
        "What was the highlight of your day today?",
        "Did I encounter any unexpected challenges or surprises?",
        "Did I learn something new or have an interesting conversation with someone today?",
        "How did I see the hand of God in my life today?",
        "Is there a specific moment from today that I would like to remember or reflect on?"
    };

    private List<Entry> entries;
    private Random random; 

    public Journal()
    {
        entries = new List<Entry>();
        random = new Random();
    }

public void AddEntry(string date)
{
    int index = random.Next(prompts.Count); 
    string prompt = prompts[index]; 

    Console.WriteLine($"Prompt: {prompt}");
    Console.Write("Enter your response: ");
    string response = Console.ReadLine(); 

    Entry entry = new Entry(prompt, response, date);
    entries.Add(entry);
}






    public void DisplayJournal()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        entries.Clear();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    entries.Add(new Entry(prompt, response, date));
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("Journal Program Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter your response: ");
                   
                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                
                      journal.AddEntry( date);
                   
                  
                    break;

                case 2:
                    Console.WriteLine("Journal Entries:");
                    journal.DisplayJournal();
                    break;

                case 3:
                    Console.Write("Enter the file name to save: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    break;

                case 4:
                    Console.Write("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    break;

                case 5:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
