namespace Aoc2023;

public class Day06: IAocDay
{
    private readonly int[] Times;
    private readonly int[] Distances;
    //2065338
    //34934171 p2
    public Day06(string input)
    {
        string[] parts = input.Split('\n');
        var nums = parts[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        Times = new int[nums.Length];
        Distances = new int[nums.Length];
        
        Times =nums;
        Distances = parts[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
    public long Part1()
    {
        long result = 1;
        for (int i = 0; i < Times.Length;i++)
        {
            int time = Times[i];
            int distance = Distances[i];
            result *= findValid(time,distance);
        }
        return result;
    }
    public long Part2()
    {
        string timeString = "";
        string distanceString = "";
        for(int i=0;i<Times.Length;i++)
        {
            timeString += Times[i];
            distanceString += Distances[i];
        }
        long distance = long.Parse(distanceString);
        long time = long.Parse(timeString);
        return findValid(time, distance);
    }

    public long findValid(long time, long distance)
    {

        long speed = 0;
        //find 1st solution of spd*time and the ans== abs(speed-time)
        while (time * speed < distance)
        {
            time--;
            speed++;
        }
        long ans = Math.Abs(speed - time) + 1;
        return ans;

    }
    
}