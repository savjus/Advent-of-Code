using System.Diagnostics;

namespace Aoc2023;

public class Day10 : IAocDay
{
    private Dictionary<Coordinate2D, char> map;
    private Coordinate2D start;
    private List<Coordinate2D> pipeCoordinates;
    private int xMax;
    private int yMax;

    public Day10(string input)
    {
        string[] lines = input.Split('\n');
        map = new Dictionary<Coordinate2D, char>();
        pipeCoordinates = new List<Coordinate2D>();

        yMax = lines.Length;
        xMax = lines[0].Length - 1;

        for (int x = 0; x < yMax; x++)
        {
            for (int y = 0; y < xMax; y++)
            {
                map.Add(new Coordinate2D(x, y), lines[x][y]);
                if (lines[x][y] == 'S')
                    start = new Coordinate2D(x, y);
            }
        }
    }

    public long Part1()
    {
        long longestDistance = 0;
        Coordinate2D prev = new Coordinate2D();
        Coordinate2D curr = start;

        while (true)
        {
            var adjacent = FindAdjacent(curr).FirstOrDefault(c => !c.Equals(prev));
            longestDistance++;

            if (adjacent == null) break;
            pipeCoordinates.Add(curr);

            prev = curr;
            curr = adjacent;

            if (curr.Equals(start)) break;
        }
        return (long)Math.Ceiling((double)longestDistance / 2);
    }

    public long Part2()
    {
        long territory = 0;
        bool isInside = false;
        char? startingJoint = null;
        // Figure out what kind of character was covered by 'S' tile.
        // Create "relative" coordinate. 
        // So coordinate directly above will be {-1, 0}, that is one row above, same column.
        // Coordinate to the right will be {0, 1}, that is same row, one column to the right.

        // For each row, go left to right. If we encounter a pipe, that means next ground tiles
        // on the other side of the tile 
        // will be inside the loop, and we count it. If we encounter another pipe, the tiles after
        // that are outside the loop,
        // and we will not count it.
        // To count pipes, |, F--J and L--7 with any number of '-' between F and J count as pipe walls.
        // -, F--7 and L--J do not count
        // as pipe walls because they run horizontal to the row and don't vertically cross the row.
        // When we're inside the loop, the pipe loop can only begin with F or L joints.
        // We can't see - 7 J if we are traversing
        // left to right, because it wouldn't be attached to anything.
        for (int x = 0; x < yMax; x++)
        {
            for (int y = 0; y < xMax; y++)
            {
                var coord = new Coordinate2D(x, y);
                char tile = map[coord];

                if (!pipeCoordinates.Contains(coord))
                {
                    // We're not in the pipe and inside the loop, possibly count the territory
                    if (isInside)
                        territory++;
                }
                else
                {
                    // We're in the pipe
                    if (tile == '|')
                    {
                        isInside = !isInside;
                    }
                    else if (tile == 'F' || tile == 'L')
                    {
                        startingJoint = tile;
                    }
                    else if (tile == 'J' || tile == '7')
                    {
                        if ((tile == 'J' && startingJoint == 'F') || (tile == '7' && startingJoint == 'L'))
                        {
                            isInside = !isInside;
                        }
                        startingJoint = null;
                    }
                }
            }
        }
        return territory;
    }

    private List<Coordinate2D> FindAdjacent(Coordinate2D curr)
    {
        var adj = new List<Coordinate2D>(2);

        // above
        if (curr.X > 0 && "S|LJ".Contains(map[curr]))
            if ("|7F".Contains(map[new Coordinate2D(curr.X - 1, curr.Y)]))
                adj.Add(new Coordinate2D(curr.X - 1, curr.Y));
        // below
        if (curr.X < yMax && "S|F7".Contains(map[curr]))
            if ("|LJ".Contains(map[new Coordinate2D(curr.X + 1, curr.Y)]))
                adj.Add(new Coordinate2D(curr.X + 1, curr.Y));
        // Left
        if (curr.Y > 0 && "S-7J".Contains(map[curr]))
            if ("L-F".Contains(map[new Coordinate2D(curr.X, curr.Y - 1)]))
                adj.Add(new Coordinate2D(curr.X, curr.Y - 1));
        // Right
        if (curr.Y < xMax && "S-LF".Contains(map[curr]))
            if ("-J7".Contains(map[new Coordinate2D(curr.X, curr.Y + 1)]))
                adj.Add(new Coordinate2D(curr.X, curr.Y + 1));

        Debug.Assert(adj.Count == 2 || adj.Count == 1);
        return adj;
    }
}
class Coordinate2D
{
    public int X;
    public int Y;

    public Coordinate2D(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    public Coordinate2D()
    {
        X = 0;
        Y = 0;
    }
    public override bool Equals(object? obj)
    {
        if (obj is Coordinate2D other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
  }
}
