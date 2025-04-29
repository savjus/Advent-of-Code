using System.Text.RegularExpressions;

namespace Aoc2023
{
    class Day01 : IAocDay
    {
        string[] inputLines;
        public Day01(string input) 
        {
            inputLines = input.Split('\n');
        }

        public long Part1()
        {
            int sum = 0;
            foreach (string line in inputLines)
            {
                sum += 10 * (line.First(char.IsDigit) - '0') + (line.Last(char.IsDigit) - '0'); 
            }
            return sum;
        }
        public long Part2()
        {
            const string PATTERN = @"one|two|three|four|five|six|seven|eight|nine|\d";
            long sum = 0;
            foreach(string line in inputLines)
            {
                var first = Regex.Match(line, PATTERN);
                var last = Regex.Match(line, PATTERN,RegexOptions.RightToLeft);
                sum += 10 * GetValue(first.Value) + GetValue(last.Value);
            }
            return sum;
        }

        private static int GetValue(string s)
        {
            return s switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => int.Parse(s)
            };
        }
    }
}