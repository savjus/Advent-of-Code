namespace Aoc2023;
class Day12 : IAocDay
{
  //operational (.) or damaged (#) unknown (?) 
  /*
  ???.### 1,1,3
  .??..??...?##. 1,1,3
  ?#?#?#?#?#?#?#? 1,3,1,6
  ????.#...#... 4,1,1
  ????.######..#####. 1,6,5
  ?###???????? 3,2,1
  */
  //          recursive backtracking
  //1. go through each line, place # or . in ?
  //  a. place # in ? --> get group size(n), 
  //    
  //     check if n chars is # or ? and
  //     the next char [n] is NOT # (so it doesn't exceed the size)
  //     (could be whitespace . or ? )
  //      
  //     then recurse with springs substring() with removed group size + 1 because seperated by .
  //     and groups with the valid one removed
  //  b. place . in ? --> skip it (recurse with substring(1))
  //  c. end if groups is empty
  //  d. end if springs is empty

  string[] lines;

  public Day12(string input)
  {
    lines = input.Split('\n');
  }

  
  long CountArrangements(string springs, List<int> groups)
  {
    if (groups.Count == 0)
      return springs.All(c => c != '#') ? 1 : 0;    // if they're all NOT # then valid, return 1
    if (springs.Length < groups[0])
      return 0;

    long count = 0;

    if (springs[0] == '.' || springs[0] == '?')
      count += CountArrangements(springs.Substring(1), groups);

    int n = groups[0];
    if (springs.Take(n).All(c => c == '#' || c == '?') &&
        (springs.Length == n || springs[n] == '.' || springs[n] == '?'))
      {
        count += CountArrangements(
          springs.Substring(Math.Min(n+1,springs.Length)),
          groups.Skip(1).ToList());
      }
    return count;
  }


  public long Part1()
  {
    long result = 0;
    foreach (string line in lines)
    {
      //get the list off numbers in each line (eg. ???.### 1,1,3)
      string[] parts = line.Split(' ');
      string springs = parts[0];
      List<int> nums = parts[1].Split(',').Select(x => int.Parse(x)).ToList();
      result += CountArrangements(springs, nums);
    }
    return result;
  }
 

  public long Part2()
  {
    long result = 0;
    foreach (string line in lines)
    {
        string[] parts = line.Split(' ');
        string springs = parts[0];
        List<int> nums = parts[1].Split(',').Select(int.Parse).ToList();

        // Unfold springs and nums
        string unfoldedSprings = string.Join("?", Enumerable.Repeat(springs, 5));
        List<int> unfoldedNums = Enumerable.Repeat(nums, 5).SelectMany(x => x).ToList();

        result += CountArrangements(unfoldedSprings, unfoldedNums);
    }
    return result;
  }
}