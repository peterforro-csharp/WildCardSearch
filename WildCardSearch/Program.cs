using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WildCardSearch
{
    /// <summary>
    /// Static Utility class for Extension methods
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// Extension method for the type: string
        /// Checks whether the string matches with the WildCard pattern given as parameter
        /// </summary>
        public static bool IsWildCardMatch(this string str, string pattern)
        {
            return Regex.IsMatch(
                input: str,
                pattern: $"^{Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".")}$"
            );
        }

        /// <summary>
        /// Extension method for the type: IEnumerable<string>
        /// Collects all the string from the collection that match with the WildCard pattern given as parameter
        /// </summary>
        public static string[] WildCardMatch(this IEnumerable<string> collection, string pattern)
        {
            return collection
                .Select(item => item)
                .Where(item => item.IsWildCardMatch(pattern))
                .ToArray();
        }

        /// <summary>
        /// Extension method for the type: IEnumberable<string>
        /// Writes the string items of the collection to the Standard Output
        /// </summary>
        public static void PrintStdOut(this IEnumerable<string> collection)
        {
            var cnt = 0;
            foreach (var item in collection)
            {
                Console.WriteLine($"\t{++cnt}.:\t{item}");
            }
        }
    }

    /// <summary>
    /// Main class of the Console Application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Reads in the WildCard pattern from the Standard input
        /// </summary>
        private static string GetPatternFromStdIn()
        {
            Console.Write("Pattern?: ");
            return Console.ReadLine();
        }

        /// <summary>
        /// Main entry point of the program
        /// </summary>
        private static void Main()
        {
            // Place the input.txt file to the WorkDirectory
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
            var data = File.ReadAllLines(filePath);

            var match = data.WildCardMatch(GetPatternFromStdIn());

            if (match.Length == 0)
            {
                Console.WriteLine("0 match found");
            }
            else
            {
                Console.WriteLine($"{match.Length} match found");
                match.PrintStdOut();
            }

            Console.ReadLine();
        }
    }
}
