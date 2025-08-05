namespace Aoc2024
{
    class Day08 : IAocDay
    {
        // antenna symbol, (x,y)  (x,y)
        Dictionary<char, List<Position>> antennas;
        HashSet<Position> Antinodes1;
        HashSet<Position> Antinodes2;

        public Day08(string input)
        {
            antennas = new Dictionary<char, List<Position>>();
            string[] lines = input.Split('\n');
            //get antenna positions
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] != '.')
                    {
                        if (!antennas.ContainsKey(lines[i][j]))
                        {
                            antennas[lines[i][j]] = [];
                        }
                        antennas[lines[i][j]].Add(new Position(i, j));
                    }
                }
            }


            Antinodes1 = new HashSet<Position>();
            Antinodes2 = new HashSet<Position>();
            int mapSize = lines.Length;

            foreach (char sign in antennas.Keys)
            {
                List<Position> positions = antennas[sign];
                foreach (Position a in positions)
                {
                    foreach (Position b in positions)
                    {
                        if (a == b)
                            continue;

                        int xDiff = a.X - b.X;
                        int yDiff = a.Y - b.Y;

                        Position antinodeA = a.Move(xDiff, yDiff);
                        Position antinodeB = b.Move(-xDiff, -yDiff);

                        if (!antinodeA.OutOfBounds(mapSize))
                        {
                            Antinodes1.Add(antinodeA);
                        }

                        if (!antinodeB.OutOfBounds(mapSize))
                        {
                            Antinodes1.Add(antinodeB);
                        }

                        while (!antinodeA.OutOfBounds(mapSize))
                        {
                            Antinodes2.Add(antinodeA);
                            antinodeA = antinodeA.Move(xDiff, yDiff);
                        }

                        while (!antinodeB.OutOfBounds(mapSize))
                        {
                            Antinodes2.Add(antinodeB);
                            antinodeB = antinodeB.Move(-xDiff, -yDiff);
                        }
                        Antinodes2.Add(a);
                        Antinodes2.Add(b);
                    }
                }
            }
        }
        public long Part1()
        {
            return Antinodes1.Count;
        }
        public long Part2()
        {
            return Antinodes2.Count;
        }
    }
}
