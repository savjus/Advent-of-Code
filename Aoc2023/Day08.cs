namespace Aoc2023;

public class Day08 : IAocDay
{
    private readonly int[] instructions;
    private readonly Dictionary<string, string[]> moves;
    public Day08(string input)
    {
        string[] lines = input.Split('\n');
        instructions = lines[0].Select(x => x == 'L' ? 0 : 1).ToArray();
        moves = lines.Skip(2)
            .Select(x => x.Split(new[] { ' ', ',', '(', ')', '=' }, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(x => x[0], x => x[1..]);
    }

    //14893
    //10241191004509
    public long Part1()
    {
        long res = 0;
        for (var node = "AAA"; node != "ZZZ"; res++)
            node = moves[node][instructions[res % instructions.Length]];
        return res;
    }

    public long Part2()
    {
        long res = 0;
        // Scan until an end node is seen twice, first index is phase,
        // index difference is period
        var frequencies =
            moves.Keys.Where(x => x[2] == 'A')
                .Select(x => FindLoopFrequency(x))
                .ToList();
        // Find harmony by moving harmony phase forward and
        // increasing harmony period until it matches all frequencies
        //https://ibb.co/GMGxDSr
        var harmonyPhase = frequencies[0].phase;
        var harmonyPeriod = frequencies[0].period;

        foreach (var freq in frequencies.Skip(1))
        {
            // Find new harmonyPhase by increasing phase in harmony period steps
            // until harmony matches freq
            for (;
                 harmonyPhase < freq.phase || (harmonyPhase - freq.phase) % freq.period != 0;
                 harmonyPhase += harmonyPeriod) ;
            // Find the new harmonyPeriod by looking for the next position
            // the harmony frequency matches freq (brute force least common multiplier)

            long sample = harmonyPhase + harmonyPeriod;
            for (; (sample - freq.phase) % freq.period != 0; sample += harmonyPeriod) ;
            harmonyPeriod = sample - harmonyPhase;
        }
        res = harmonyPhase;
        
        return res;
    }
    
    
    (long phase, long period) FindLoopFrequency(string node)
    {
        var endSeen = new Dictionary<string, long>();
        for (long i = 0; ; i++)
        {
            if (node[2] == 'Z')
            {
                if (endSeen.TryGetValue(node, out var lastSeen))
                    return (lastSeen, i - lastSeen);
                else
                    endSeen[node] = i;
            }
            node = moves[node][instructions[i % instructions.Length]];
        }
    }

}
