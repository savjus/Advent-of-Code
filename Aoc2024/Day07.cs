namespace Aoc2024
{
    class Day07 : IAocDay
    {
        string[] lines;
        public Day07(string input)
        {
            lines = input.Split('\n');   
        }
        public long Part1()
        {
            long ans = 0;
            foreach (string line in lines)
            {
                // Parse lines
                string[] sides = line.Split(':');
                long goal = long.Parse(sides[0]);
                List<long> values = sides[1].Trim()
                    .Split(' ')
                    .Select(x => long.Parse(x))
                    .ToList();
                // Check 
                Console.WriteLine(goal+ "        ");
                foreach (var a in values)
                {
                    Console.Write(a + " ");
                }
                Console.Write(goal);
            }
            return ans;
        }
        public long Part2()
        {
            long ans = 0;
            return ans;   
        }
    }
}
