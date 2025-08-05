namespace Aoc2024
{
    class Day13 : IAocDay
    {
        string []input;
        public Day13(string input)
        {
            this.input = input.Split('\n');
        }
        public long Part1()
        {
            (long, long) A;
            (long, long) B;
            (long, long) Prize;
            foreach(string line in input)
            {
                A = ParseButton(line);
            }
            return 0;
        }
        (long, long) ParseButton(string line) {
            (long, long) result = (0, 0);
            return result;
        }
        public long Part2()
        {
            return 0;
        }

    }
}
