using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2024
{
    class Day01 : IAocDay
    {
        List<int> first;
        List<int> second;
        public Day01(string input) 
        {
            string[] lines = input.Split('\n');
            first = new List<int>();
            second = new List<int>();
            foreach (string line in lines)
            {
                first.Add(int.Parse(line.Split(' ',StringSplitOptions.RemoveEmptyEntries)[0]));
                second.Add(int.Parse(line.Split(' ',StringSplitOptions.RemoveEmptyEntries)[1]));
            }
                first.Sort();
                second.Sort();
        }
        public long Part1()
        {
            long result = 0;
            for (int i = 0;i<first.Count;i++)
            {
                result += Math.Abs(first[i] - second[i]);
            }
            return result;
        }
        public long Part2()
        {
            long result = 0;
            for(int i = 0; i < first.Count; i++)
            {
                result += second.Count(x => x == first[i]) * first[i]; 
            }
            return result;
        }
    }
}
