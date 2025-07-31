    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal class Day02 : IAocDay
    {
        string[] inputlines;
        public Day02(string input)
        {
            inputlines = input.Split('\n');
        }

        public long Part1()
        {
            long sum = 0;
            Dictionary<string, int> sets = new Dictionary<string, int>
            {{ "red", 12 },
            { "green", 13 },
            { "blue", 14 }};

            for (int id = 0; id < inputlines.Length; id++)
            {
                string line = inputlines[id].Trim();
                //if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);

                int gameId = int.Parse(parts[0].Split(' ')[1]);
                var games = parts[1].Split(';', StringSplitOptions.RemoveEmptyEntries);

                bool isValid = true;

                foreach (var game in games)
                {
                    var words = game.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length - 1; i += 2)
                    {
                        int num = int.Parse(words[i]);
                        string color = words[i + 1];

                        if (sets[color] < num)
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (!isValid) break;
                }

                if (isValid)
                {
                    sum += gameId;
                }
            }

            return sum;
        }

        public long Part2()
        {
            long sum = 0;
            for (int id = 0; id < inputlines.Length; id++)
            {
                string line = inputlines[id].Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;


                var parts = line.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);
                int gameId = int.Parse(parts[0].Split(' ')[1]);
                var games = parts[1].Split(';', StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, int> sets = new Dictionary<string, int>();
                //sets.Clear();
                foreach (var game in games)
                {
                    var words = game.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < words.Length - 1; i += 2)
                    {
                        int num = int.Parse(words[i]);
                        string color = words[i + 1];

                        if (!sets.ContainsKey(color))
                        {
                            sets.Add(color, num);
                        }

                        if(sets[color] < num)
                        {
                            sets[color] = num;
                        }
                    }

                }
                long pow = 1;
                foreach(var set in sets)
                {
                    pow *= set.Value;
                }
                sum += pow;
            }

            return sum;
        }
    }
}
