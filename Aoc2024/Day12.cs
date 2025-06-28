using System.Data.Common;
using System.Diagnostics;

namespace Aoc2024
{
    class Day12 : IAocDay
    {
        string[] input;
        int[][] directions =
        {
            [0, 1],
            [0,-1],
            [1, 0],
            [-1,0],
        };
        public Day12(string input)
        {
            this.input = input.Split('\n');
        }

        //size of island * islands perimiter
        // get list<int,int> postitions
        // list.count() = island size
        // ???? = island perimiter
        public long Part1()
        {
            long result = 0;
            Dictionary<char, List<Position>> islands = new Dictionary<char,List<Position>>();
            HashSet<Position> visited = new HashSet<Position>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    Position pos = new Position(i, j);
                    if (!visited.Contains(pos))
                    {
                        char cur = input[i][j];
                        islands[cur] = DFS(input, pos, cur, new HashSet<Position>());
                        foreach (var lp in islands.Values)
                            foreach(var p in lp)
                                visited.Add(p);
                        int perimiter = CountPerimeter(islands[cur]);
                        result += islands[cur].Count() * perimiter;
                        System.Console.WriteLine($"{islands[cur].Count()} {perimiter}");
                    }
                }
            }
            return result;
        }
        
        //instead of perimeter find side count
        public long Part2()
        {
            long result = 0;
            Dictionary<char, List<Position>> islands = new Dictionary<char, List<Position>>();
            HashSet<Position> visited = new HashSet<Position>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    Position pos = new Position(i, j);
                    if (!visited.Contains(pos))
                    {
                        char cur = input[i][j];
                        islands[cur] = DFS(input, pos, cur, new HashSet<Position>());
                        foreach (var lp in islands.Values)
                            foreach (var p in lp)
                                visited.Add(p);
                        int sides = ;
                        result += islands[cur].Count() * sides;
                        System.Console.WriteLine($"{islands[cur].Count()} {sides}");
                    }
                }
            }
            return result;
        }
       
        public int CountPerimeter(List<Position> island)
        {
            HashSet<Position> set = new HashSet<Position>(island);
            int perimeter = 0;
            foreach (Position pos in island)
            {
                foreach (int[] dir in directions)
                {
                    int nx = pos.X + dir[0];
                    int ny = pos.Y + dir[1];

                    if (!set.Contains(new Position(nx, ny)))
                    {
                        perimeter++;
                    }
                }
            }
            return perimeter;
        }
        public List<Position> DFS(string[] input, Position startPos, char c,HashSet<Position> visited)
        {
            if (visited == null) {
                visited = new HashSet<Position>();
            }
            List<Position> island = new List<Position>();
            if (visited.Contains(startPos))
                return island;
            visited.Add(startPos);
            island.Add(startPos);
            foreach (int[] dir in directions)
            {
                int nx = startPos.X + dir[0];
                int ny = startPos.Y + dir[1];

                if (nx < 0 || ny < 0 || nx > input.Length - 1 || ny > input[0].Length - 1)
                    continue;

                if (input[nx][ny] == c && !visited.Contains(new Position(nx,ny)))
                {
                    island.AddRange(DFS(input,new Position(nx,ny),c,visited));
                }
            }
            return island;
        }
    }

    class Postition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Postition(int x, int y)
        {
            X = X;
            Y = Y;
        }
        public override bool Equals(object? obj)
        {
            return obj is Position other && X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }
    }
}
