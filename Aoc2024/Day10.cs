﻿using System.ComponentModel.DataAnnotations;

namespace Aoc2024
{
    class Day10 : IAocDay
    {
        int[][] input;
        int[][] directions =
           {
               [0,1],
               [0,-1],
               [1,0],
               [-1,0],
            };
        public Day10(string input)
        {
            this.input = input.Split('\n').Select(l => l.ToCharArray().Select(c => c - '0').ToArray()).ToArray();
        }


        public long Part1()
        {
            long result = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 0)
                    {
                        HashSet<(int, int)> reachableNines = new HashSet<(int, int)>();
                        DFSPart1(i, j, reachableNines);
                        result += reachableNines.Count; 
                    }
                }
            }
            return result;
        }
         //dfs called recursively
        private void DFSPart1(int row, int col,HashSet<(int,int)> reachableNines)
        {
            // System.Console.WriteLine($"{row} {col}   value {input[row][col]}");
            if (input[row][col] == 9)
            {
                reachableNines.Add((row,col));
                return;
            }

            foreach (int[] dir in directions)
            {
                int newRow = row + dir[0];
                int newCol = col + dir[1];

                if (newRow < 0 || newCol < 0 || newRow >= input.Length || newCol >= input[0].Length)
                    continue;

                if (input[newRow][newCol] == input[row][col] + 1)
                {
                    DFSPart1(newRow, newCol,reachableNines);
                }
            }
        }

        public long Part2()
        {
            long result = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 0)
                    {
                        DFS(i, j, ref result);
                    }
                }
            }
            return result;
        }
        //dfs called recursively
        private void DFS(int row, int col,ref long score)
        {
            // System.Console.WriteLine($"{row} {col}   value {input[row][col]}");
            if (input[row][col] == 9)
            {
                score++;
                return;
            }

            foreach (int[] dir in directions)
            {
                int newRow = row + dir[0];
                int newCol = col + dir[1];

                if (newRow < 0 || newCol < 0 || newRow >= input.Length || newCol >= input[0].Length)
                    continue;

                if (input[newRow][newCol] == input[row][col] + 1)
                {
                    DFS(newRow, newCol,ref score);
                }
            }
        }
    }
}
