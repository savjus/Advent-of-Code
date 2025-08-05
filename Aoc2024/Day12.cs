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
                        // System.Console.WriteLine($"{islands[cur].Count()} {perimiter}");
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
                        foreach (List<Position> lp in islands.Values)
                            foreach (Position p in lp)
                                visited.Add(p);
                        int sides = CountSides(islands[cur]);
                        result += islands[cur].Count() * sides;
                        // System.Console.WriteLine($"{islands[cur].Count()} {sides}");
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// checks up down sides, then left right.
        /// counts by ordering the positions, then
        /// checking neighbor position.
        /// if there is no island to the side --> its a side
        /// check if the side wasnt already discovered (using prevY or prevX here)
        /// If not, its a new side so +1
        ///     o X
        ///     o X
        ///     o X X X X
        ///     o o o o o
        /// </summary>
        /// <param name="island">list of positions making up the island</param>
        /// <returns>number of sides on an island</returns>
        public int CountSides(List<Position> island)
        {
            var set = new HashSet<Position>(island);
            int sides = 0;

            // check left and right sides 
            //group into rows eg. Col 0: [1,2,3,5,6] Col 1: [2,4,5]
            var groupedByCol = island.GroupBy(pos => pos.X);
            foreach (var col in groupedByCol)
            {
                // sort Y coords in ascending order
                List<int> yList = col.Select(pos => pos.Y).OrderBy(y => y).ToList();
                // For checking left and right position in the row
                foreach (int dirX in new int[] { -1, 1 })
                {
                    int prevY = int.MinValue;
                    foreach (int y in yList)
                    {
                        int ny = y;
                        int nx = col.Key + dirX; // here for readability
                        if (!set.Contains(new Position(nx, ny)))
                        {
                            if (prevY != y - 1)
                                sides++;
                            prevY = y;
                        }
                        else
                        {
                            prevY = int.MinValue;
                        }
                    }
                }
            }

            //check above and below sides
            //group by Y to make rows
            var groupedByRow = island.GroupBy(pos => pos.Y);
            foreach (var row in groupedByRow)
            {
                // sort the rows x positions
                List<int> xList = row.Select(pos => pos.X).OrderBy(x => x).ToList();
                // above and below
                foreach (int dirY in new int[] { 1, -1 })
                {
                    int prevX = int.MinValue;
                    foreach (int x in xList)
                    {
                        int nx = x;
                        int ny = row.Key + dirY;
                        if (!set.Contains(new Position(nx, ny)))
                        {
                            if (prevX != x - 1)
                                sides++;
                            prevX = x;
                        }
                        else
                        {
                            prevX = int.MinValue;
                        }
                    }   
                }
            }

            return sides;
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
}
