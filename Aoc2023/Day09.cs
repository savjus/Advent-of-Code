namespace Aoc2023;

public class Day09 : IAocDay
{
    private readonly string []lines;
    public Day09(string input)
    {
        lines = input.Split('\n');
    }

    public long Part1()
    {
        long sum = 0;
        for (int i = 0; i < lines.Length;i++)
        {
            List<List<long>> iterations = new List<List<long>>();
            iterations.Add(lines[i].Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToList());

            while (iterations.Last().Any(x => x != 0))
            {
                List<long> nextIteration = new List<long>();
                List<long> prevIteration = iterations.Last();
                for (int j = 0; j < prevIteration.Count - 1; j++)
                {
                    nextIteration.Add(prevIteration[j+1]-prevIteration[j]);
                }   
                iterations.Add(nextIteration);
            }
            //we have all iterations
            //start from 2nd from end, get the number, add the number to end of other iterations 
            long nextNum = 0;
            // take last iteration
            for (int j = iterations.Count-2; j >= 0; j--)
            {
                //take last number
                long lastNum = iterations[j][iterations[j].Count - 1];
                //add it to last number of other last iteration
                nextNum = nextNum + lastNum;
            }
            sum += nextNum;
        }
        return sum;
    }
    public long Part2()
    {
        long sum = 0;
        for (int i = 0; i < lines.Length;i++)
        {
            List<List<long>> iterations = new List<List<long>>();
            iterations.Add(lines[i].Split(' ',StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToList());

            while (iterations.Last().Any(x => x != 0))
            {
                List<long> nextIteration = new List<long>();
                List<long> prevIteration = iterations.Last();
                for (int j = 0; j < prevIteration.Count - 1; j++)
                {
                    nextIteration.Add(prevIteration[j+1]-prevIteration[j]);
                }   
                iterations.Add(nextIteration);
            }
            //start from 2nd from end, get the first number, subtract the number from start of other iterations 
            long nextNum = 0;
            for (int j = iterations.Count-2; j >= 0; j--)
            {
                long lastNum = iterations[j][0];
                nextNum =  lastNum-nextNum;
            }
            sum += nextNum;
        }
        return sum;
    }
}