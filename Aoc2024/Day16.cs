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
            return findCheapestPathCost(moveCost: 1, turnCost: 1000, out long sharedTileCount);
        }
        public long Part2()
        {
            findCheapestPathCost(moveCost: 1, turnCost: 1000, out long sharedTileCount);
            return sharedTileCount;
        }

        private long findCheapestPathCost(int moveCost, int turnCost, out long sharedTileCount)
        {
            // begins facing east 
            Position startFacing = new Position(-1, 0);

            Dictionary<(Position pos,Position dir),long> minCostForState = new Dictionary<(Position pos, Position dir), long>();
            PriorityQueue<(Position pos, Position dir), long> priorityQueue = new PriorityQueue<(Position pos, Position dir), long>();

            (Position,Position) startState = (current, startFacing);
            minCostForState[startState] = 0;
            priorityQueue.Enqueue(startState, 0);

            long bestEndCost = long.MaxValue;

            while (priorityQueue.Count > 0)
            {
                priorityQueue.TryDequeue(out var state, out var cost);

                if (state.pos.Equals(end))
                {
                    if (cost < bestEndCost)
                        bestEndCost = cost;
                }

                foreach (var dir in directions)
                {
                    int quarterTurns = findTurnCount(state.dir, dir);
                    long nextCost = cost + turnCost * quarterTurns + moveCost;

                    if (nextCost > bestEndCost) continue;

                    Position newPos = state.pos + dir;
                    if (newPos.OutOfBounds(gridHeight, gridWidth) || walls.Contains(newPos))
                        continue;

                    (Position,Position) key = (newPos, dir);
                    if (!minCostForState.TryGetValue(key, out long oldCost) || nextCost < oldCost)
                    {
                        minCostForState[key] = nextCost;
                        priorityQueue.Enqueue(key, nextCost);
                    }
                }
            }

            // Backtrack from all end states with minimal cost to count tiles on any cheapest path
            var tiles = new HashSet<Position>();
            var seenStates = new HashSet<(Position pos, Position dir)>();
            var pathQueue = new Queue<(Position pos, Position dir)>();

            foreach (var kv in minCostForState)
            {
                if (kv.Key.pos.Equals(end) && kv.Value == bestEndCost)
                {
                    if (seenStates.Add(kv.Key))
                        pathQueue.Enqueue(kv.Key);
                }
            }

            while (pathQueue.Count > 0)
            {
                (Position pos,Position dir) to = pathQueue.Dequeue();
                tiles.Add(to.pos);

                Position fromPos = to.pos - to.dir;
                if (fromPos.OutOfBounds(gridHeight, gridWidth) || walls.Contains(fromPos))
                    continue;

                foreach (var fromDir in directions)
                {
                    var fromState = (fromPos, fromDir);
                    if (!minCostForState.TryGetValue(fromState, out long fromCost)) continue;

                    int turns = findTurnCount(fromDir, to.dir);
                    long edgeCost = turnCost * turns + moveCost;
                    if (fromCost + edgeCost == minCostForState[to])
                    {
                        if (seenStates.Add(fromState)) pathQueue.Enqueue(fromState);
                    }
                }
            }

            sharedTileCount = tiles.Count;
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
