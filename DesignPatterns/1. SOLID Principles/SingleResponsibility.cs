using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DesignPatterns
{
    /**
     * Single Responsibility Principle is where a class should only have ONE 
     * reason to change. This is done by Seperation of concerns resulting
     * in different classes handling different independent tasks/problems
     * 
     * The Journal class is responsible for handling the journal.
     * This includes adding and removing entries.
     * In this case it also provides a way of displaying the entries.
     */ 
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    /**
     * The Persistence class is responsible for handling the persistence of the data.
     * In this case it saves to a file, but it could be to a database or something
     * similar.
     * 
     * The important thing is to achieve seperation of concerns, so that each class
     * only have a single responsibility.
     */ 
    public class Persistence
    {
        public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }

    public class SingleResponsibility
    {
        //public static void Main(string[] args)
        //{
        //    var j = new Journal();
        //    j.AddEntry("I cried today");
        //    j.AddEntry("But tomorrow will be better..");
        //    Console.WriteLine(j);

        //    var p = new Persistence();
        //    var filename = @"C:\temp\journal.txt";
        //    p.SaveToFile(j, filename, true);
        //    Process.Start(filename);
        //}
    }
}
