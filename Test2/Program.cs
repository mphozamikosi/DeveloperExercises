using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Test2
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            Console.Write("Please enter the input string: ");
            string input = Console.ReadLine();
            int result = Add(input);
            Console.WriteLine($"The sum of the numbbers is: {result}");
        }

        public static int Add(string numberSequence)
        {
            char delimeter = ',';
            int sum = 0;
            try
            {
                if (!string.IsNullOrEmpty(numberSequence))
                {
                    if (numberSequence.StartsWith("//"))
                    {
                        delimeter = ExtractDelimeter(ref numberSequence);
                    }

                    string[] numberLines = numberSequence.Split('\n');
                    foreach(string numberLine in numberLines)
                    {
                        sum = ProcessNumberLines(delimeter, sum, numberLine);
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return sum;
        }

        private static char ExtractDelimeter(ref string numberSequence)
        {
            char delimeter = char.Parse(numberSequence.Substring(2, 1));
            numberSequence = numberSequence.Substring(3, numberSequence.Length - 3);
            return delimeter;
        }

        private static int ProcessNumberLines(char delimeter, int sum, string numberLine)
        {
            if (!string.IsNullOrEmpty(numberLine))
            {
                int[] numbers = Array.ConvertAll(numberLine.Split(delimeter), int.Parse);
                if (!numbers.Any(x => x < 0))
                    if (!(numbers.Length > 2))
                    {
                        sum += numbers.Sum();
                    }
                    else
                    {
                        throw new Exception("Error: There are more than 2 numbers in the line");
                    }
                else
                    throw new Exception("Error: Negatives are not allowed.");
            }

            return sum;
        }
    }
}