namespace Aoc2024
{
    class Day16 : IAocDay
    {
        HashSet<Position> walls = new HashSet<Position>();
        Position end = new Position();
        Position current = new Position();
        int gridWidth;
        int gridHeight;
        readonly List<Position> directions = new List<Position>
        {
            new Position(1,0),   // Right
            new Position(0,-1),  // Up
            new Position(-1,0),  // left
            new Position(0,1)    // Down
        };
        public Day16(string input)
        {
            string[] lines = input.Split('\n');
            parseGrid(lines);

        }
        public long Part1()
        {
            return findCheapestPathCost(moveCost: 1, turnCost: 1000);
        }
        public long Part2()
        {
          
        }
        private long findCheapestPathCost(int moveCost, int turnCost)
        {
            // begins from facing east
            Position startFacing = new Position(-1, 0);

            var minCostForState = new Dictionary<(Position pos, Position dir), long>();
            var queue = new PriorityQueue<(Position pos, Position dir), long>();

            var startState = (current, startFacing);
            minCostForState[startState] = 0;
            queue.Enqueue(startState, 0);

            long bestEndCost = long.MaxValue;

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var state, out var cost);
                if (state.pos.Equals(end))
                {
                    if (cost < bestEndCost)
                    {
                        bestEndCost = cost;
                    }
                }

                foreach (var direction in directions)
                {
                    int quarterTurns = findTurnCount(state.dir, direction);
                    long turnPrice = turnCost * quarterTurns;
                    long nextCost = cost + turnPrice + moveCost;

                    Position newPos = state.pos + direction;

                    if (newPos.OutOfBounds(gridHeight,gridWidth) || walls.Contains(newPos))
                        continue;

                    (Position,Position) key = (newPos, direction);
                    if (!minCostForState.TryGetValue(key, out long oldCost) || nextCost < oldCost)
                    {
                        minCostForState[key] = nextCost;
                        queue.Enqueue(key, nextCost);
                    }
                }
            }
            return bestEndCost;
        }


        private static int findTurnCount(Position fromDir, Position toDir)
        {
            if (fromDir.X == toDir.X && fromDir.Y == toDir.Y) return 0;
            if (fromDir.X == -toDir.X && fromDir.Y == -toDir.Y) return 2;
            return 1;
        }

        private void parseGrid(string[] lines)
        {
            gridHeight = lines.Length;
            gridWidth = lines[0].Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    switch (c)
                    {
                        case '#':
                            walls.Add(new Position(j, i));
                            break;
                        case 'S':
                            current = new Position(j, i);
                            break;
                        case 'E':
                            end = new Position(j, i);
                            break;
                    }
                }
            }
        }
    }
}
