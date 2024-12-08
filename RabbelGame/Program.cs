using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SpelaRabbel
{
    internal class Program
    {
        static char[,] LettersInGame()
        {
            char[,] grid = {
           {'T', 'N', 'N', 'G'},
           {'T', 'A', 'E', 'H'},
           {'M', 'V', 'V', 'N'},
           {'Ä', 'A', 'I', 'S'}
        };
        return grid;
        }
        static void Main()
        {
            Dictionary<string, int> index = DeserializeJSON();
            char[,] grid = LettersInGame();
            List<string> foundWords = FindWords(grid, index);

            Console.WriteLine("Found words:");
            foreach (string word in foundWords)
            {
                Console.WriteLine(word);
            }
        }
        static Dictionary<string, int> DeserializeJSON()
        {
            string json = File.ReadAllText("words.json");
            Dictionary<string, int>? index = null;
            if (json != null)
            {
                index = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            }
            if (index != null)
            {
            return index;
            }
            else
            {
            Environment.Exit(0);
            return null;
            }
        }
        static List<string> FindWords(char[,] grid, Dictionary<string, int> index)
        {
            List<string> foundWords = new List<string>();
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    SearchWords(grid, index, foundWords, "", i, j, new bool[rows, cols]);
                }
            }
            return foundWords;
        }
        static void SearchWords(char[,] grid, Dictionary<string, int> index, List<string> foundWords, string currentWord, int row, int col, bool[,] visited)
        {
                int rows = grid.GetLength(0);
                int cols = grid.GetLength(1);


                visited[row, col] = true;


                currentWord += grid[row, col];


                if (currentWord.Length >= 3 && currentWord.Length <= 16 && index.ContainsKey(currentWord))
            {
                    foundWords.Add(currentWord);
            }


            for (int i = row - 1; i <= row + 1; i++)
                {
                for (int j = col - 1; j <= col + 1; j++)
                {

                    if (i >= 0 && i < rows && j >= 0 && j < cols && !visited[i, j])
                    {

                    SearchWords(grid, index, foundWords, currentWord, i, j, visited);
                    Console.WriteLine(currentWord);
                    }
                    }
                }


            visited[row, col] = false;
            currentWord = currentWord.Substring(0, currentWord.Length - 1);
        }
    }
}