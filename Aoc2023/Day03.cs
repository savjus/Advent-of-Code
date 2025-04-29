using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Aoc2023
{
    internal class Day03 : IAocDay
    {
        //p1 534848 too low
        //p2 84676048 too low
        string[] inputLines;

        public Day03(string input)
        {
            inputLines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        public long Part1()
        {
            long sum = 0;
            List<Position> symbols =  GetGrid();
            HashSet<Position> countedNumbers = new HashSet<Position>();
            // check around each symbol for number, once found go to number start -> get number, add the positions into cache to not check again.
            foreach (Position symbol in symbols)
            {
                foreach (var direction in symbol.Get8Around())
                {
                    int i = direction.X;
                    int j = direction.Y;
                    if (i >= 0 && i < inputLines.Length && j >= 0 && j < inputLines[i].Length && char.IsDigit(inputLines[i][j]) && !countedNumbers.Contains(new Position(i, j)))
                    {
                        // Move left to find the start of the number
                        int startJ = j;
                        while (startJ > 0 && char.IsDigit(inputLines[i][startJ - 1]))
                        {
                            startJ--;
                        }
                        
                        // Extract full number
                        string num = "";
                        int currentJ = startJ;
                        while (currentJ < inputLines[i].Length && char.IsDigit(inputLines[i][currentJ]))
                        {
                            num += inputLines[i][currentJ];
                            countedNumbers.Add(new Position(i, currentJ)); // Mark all parts of the number as counted
                            currentJ++;
                        }
                        int number = int.Parse(num);
                        sum += number;
                    }
                }
            }
            return sum;
        }

        public long Part2()
        {
            long sum = 0;
            List<Position> symbols =  GetGrid();
            List<int> gearnum = new List<int>();
            HashSet<Position> countedNumbers = new HashSet<Position>();
            foreach (Position symbol in symbols)
            {
                bool gear = false;
                if (inputLines[symbol.X][symbol.Y] == '*')
                {
                    gearnum.Clear();
                    gear = true;
                }
                if (!gear)
                {
                    continue;
                }
                foreach (var direction in symbol.Get8Around())
                {
                    int i = direction.X;
                    int j = direction.Y;
                    if (i >= 0 && i < inputLines.Length && j >= 0 && j < inputLines[i].Length && char.IsDigit(inputLines[i][j]) && !countedNumbers.Contains(new Position(i, j)))
                    {
                        int startJ = j;
                        while (startJ > 0 && char.IsDigit(inputLines[i][startJ - 1]))
                        {
                            startJ--;
                        }
                        string num = "";
                        int currentJ = startJ;
                        while (currentJ < inputLines[i].Length && char.IsDigit(inputLines[i][currentJ]))
                        {
                            num += inputLines[i][currentJ];
                            countedNumbers.Add(new Position(i, currentJ)); // Mark all parts of the number as counted
                            currentJ++;
                        }
                        int number = int.Parse(num);
                        gearnum.Add(number);   
                    }
                }
                if (gear)
                {
                    if (gearnum.Count > 1)
                    {
                        long times = 1;
                        foreach (var num in gearnum)
                        {
                            times *= num;
                        }
                        sum += times;
                    }
                }
            }
            return sum; //84883664
        }

        public List<Position> GetGrid()
        {
            List<Position> symbols = new List<Position>();
            for (int i = 0; i < inputLines.Length; i++)
                for (int j = 0; j < inputLines[i].Length; j++)
                    if (!char.IsDigit(inputLines[i][j]) && inputLines[i][j] != '.')
                        symbols.Add(new Position(i, j));
            
            return symbols;
        }
    }
    class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public List<Position> Get8Around()
        {
            return new List<Position>()
                {
                    new Position(X - 1, Y - 1),
                    new Position(X - 1, Y),
                    new Position(X - 1, Y + 1),
                    new Position(X, Y - 1),
                    new Position(X, Y + 1),
                    new Position(X + 1, Y - 1),
                    new Position(X + 1, Y),
                    new Position(X + 1, Y + 1)
                };
        }

        public override bool Equals(object obj)
        {
            if (obj is Position other)
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
}