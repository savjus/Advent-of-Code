using System.Runtime.InteropServices.Marshalling;

namespace Aoc2024
{
    class Day11 : IAocDay
    {
        // 0->1
        //even digits = left half and right half
        //else -> old number * 2024

        //number of blinks important
        //count number of stones
        string _input;
        public Day11(string input)
        {
            _input = input;
        }
        //SLOW
        private long Blinks(string input, int blinkCount)
        {
            Dictionary<long, long> stones = input
            .Split(' ')
            .Select(long.Parse)
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => (long)g.Count());
            for (int i = 0; i < blinkCount; i++)
            {
                var newstones = new Dictionary<long, long>();
                foreach (var kvp in stones)
                {
                    long stone = kvp.Key;
                    long count = kvp.Value;

                    if (stone == 0)
                    {
                        if (!newstones.ContainsKey(1))
                            newstones[1] = 0;
                        newstones[1] += count;
                        continue;
                    }
                    //get digit count, if even split into 2 while keeping all stones in line
                    int digits = (int)Math.Floor(Math.Log10(stone) + 1);

                    if (digits % 2 == 0)
                    {
                        long devisor = (long)Math.Pow(10, digits / 2);
                        long left = stone / devisor;
                        long right = stone % devisor;
                        // System.Console.WriteLine($"{stones[j]}     {left} {right}");

                        if (!newstones.ContainsKey(left))
                            newstones[left] = 0;
                        newstones[left] += count;

                        if (!newstones.ContainsKey(right))
                            newstones[right] = 0;
                        newstones[right] += count;
                    }
                    else
                    {
                        long newStone = stone * 2024;
                        if (!newstones.ContainsKey(newStone))
                            newstones[newStone] = 0;
                        newstones[newStone] += count;
                    }
                }
                stones = newstones;
            }
            return stones.Values.Sum();
        }


        public long Part1()
        {
            return Blinks(_input, 25);
        }
        public long Part2()
        {
            return Blinks(_input, 75);
        }
    }
}
