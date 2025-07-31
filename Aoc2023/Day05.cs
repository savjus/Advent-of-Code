namespace Aoc2023;

public class Day05 : IAocDay
{
    private string[] chunks;
    private List<(long destination, long source, long length)> maps;

    public Day05(string input)
    {
        chunks = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
    }

    public long Part1()
    {
        // Parse seeds
        var seeds = chunks[0].Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
        
        seeds = GetFinalLocation(seeds);
        return seeds.Min();
    }
    public long Part2()
    {
        var seedRanges = chunks[0].Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
    
        List<(long start, long length)> ranges = new();
        for (int i = 0; i < seedRanges.Length; i += 2)
        {
            ranges.Add((seedRanges[i], seedRanges[i + 1]));
        }
    
        ranges = GetFinalLocationForRanges(ranges);
    
        return ranges.Min(r => r.start); // The smallest starting point after transformation
    }

   public List<(long start, long length)> GetFinalLocationForRanges(List<(long start, long length)> ranges)
    {
        maps = new List<(long destination, long source, long length)>();
        
        // Process mapping
        for (int i = 1; i < chunks.Length; i++)
        {
            maps.Clear();
            string[] lines = chunks[i].Split('\n');
            
            for (int j = 1; j < lines.Length; j++)
            {
                long[] parts = lines[j].Split(' ').Select(long.Parse).ToArray();
                maps.Add((parts[0], parts[1], parts[2]));
            }
            
            List<(long start, long length)> newRanges = new();
            
            foreach (var (start, length) in ranges)
            {
                long currentStart = start;
                long remainingLength = length;
                
                while (remainingLength > 0)
                {
                    bool mapped = false;
                    foreach (var (destination, source, mapLength) in maps)
                    {
                        if (currentStart >= source && currentStart < source + mapLength)
                        {
                            long offset = currentStart - source;
                            long chunkSize = Math.Min(mapLength - offset, remainingLength);
                            newRanges.Add((destination + offset, chunkSize));
                            currentStart += chunkSize;
                            remainingLength -= chunkSize;
                            mapped = true;
                            break;
                        }
                    }
                    
                    if (!mapped) // No mapping applied, keep original value
                    {
                        long nextMapStart = maps.
                            Where(m => m.source > currentStart)
                            .Select(m => m.source)
                            .DefaultIfEmpty(long.MaxValue)
                            .Min();
                        long chunkSize = Math.Min(nextMapStart - currentStart, remainingLength);
                        newRanges.Add((currentStart, chunkSize));
                        currentStart += chunkSize;
                        remainingLength -= chunkSize;
                    }   
                }
            }
            
            ranges = newRanges;
        }
        
        return ranges;
    }

    public long[] GetFinalLocation(long[] seeds) // handles small ammount of seeds (<100 i'd say)
    {
        maps = new List<(long destination, long source, long length)>();
        

        // Process mapping
        for (int i = 1; i < chunks.Length; i++)
        {
            maps.Clear();
            string[] lines = chunks[i].Split('\n');
            
            for (int j = 1; j < lines.Length; j++)
            {
                long[] parts = lines[j].Split(' ').Select(long.Parse).ToArray();
                maps.Add((parts[0], parts[1], parts[2]));
            }
            
            // Apply mappings efficiently
            for (int k = 0; k < seeds.Length; k++)
            {
                long seed = seeds[k];
                foreach (var (destination, source, length) in maps)
                {
                    if (seed >= source && seed < source + length)
                    {
                        seeds[k] = destination + (seed - source);
                        break;
                    }
                }
            }
        }

        return seeds;
    }
}
