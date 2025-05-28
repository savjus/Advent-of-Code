using System.Text.RegularExpressions;

namespace Aoc2024
{
    class Day03 : IAocDay
    {
        Regex pattern1 = new Regex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)", RegexOptions.Multiline);
        Regex pattern2 = new Regex(@"mul\(([0-9]{1,3}),([0-9]{1,3})\)|do\(\)|don't\(\)", RegexOptions.Multiline);
        string lines;
        public Day03(string input)
        {
            lines = input;
        }

        public long Part1()
        {
            long result = 0;
            MatchCollection matches = pattern1.Matches(lines);
            foreach (Match match in matches)
            {
                int n1 = int.Parse(match.Groups[1].Value);
                int n2 = int.Parse(match.Groups[2].Value);
                result += n1 * n2;
            }
            return result;
        }

        public long Part2()
        {
            long result = 0;
            bool enabled = true;
            var matches = pattern2.Matches(lines);
            foreach (Match match in matches)
            {
                if (match.Value.StartsWith("do()"))
                    enabled = true;
                else if (match.Value.StartsWith("don't()"))
                    enabled = false;
                else if (enabled && match.Groups.Count > 2)
                {
                    int n1 = int.Parse(match.Groups[1].Value);
                    int n2 = int.Parse(match.Groups[2].Value);
                    result += n1 * n2;
                }
            }
            return result;
        }
    }
}
