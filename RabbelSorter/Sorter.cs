using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Sorter // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main()
        {
            string filePath = @"F:\git\Rabbel\swedish.txt";
            List<string> words = new List<string>();

            try 
            {
                using (StreamReader textReader = new StreamReader(filePath))
                {
                    string line;
                    int lineCount = 0;
                    int acctualLinesAdded = 0;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        if (line.Length >= 3 && line.Length <= 16 && !line.Contains('-') && !line.Contains(' '))
                        {
                            words.Add(line);
                            acctualLinesAdded++;
                        }
                        else{Console.WriteLine("Line did not meet criteria");}
                        lineCount++;
                        Console.WriteLine("Number of lines read: " + lineCount);
                    }
                    Console.WriteLine("Lines that meet the criteria: " + acctualLinesAdded);
                }
                // Output the contents of the list
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }
                Dictionary<string, int> index = BuildIndex(words);
                SerializeIndex(index, "index.json");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Filen hittades inte: " + filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Ett fel upstod vid läsningen av filen: " + ex.Message);
            }
        }
        static Dictionary<string, int> BuildIndex(List<string> words)
        {
            Dictionary<string, int> index = new Dictionary<string, int>();
            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i];
                if (!index.ContainsKey(word))
                {
                    index[word] = i;
                }
            }
            return index;
        }
        static void SerializeIndex(Dictionary<string, int> index, string filePathJSON)
        {
            string json = JsonSerializer.Serialize(index);
            File.WriteAllText(filePathJSON, json);
        }
    }
}