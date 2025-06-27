using System.ComponentModel.DataAnnotations;

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
            this.input = input.Split('\n').Select(l => l.ToCharArray().Select(c => c -'0').ToArray()).ToArray();
        }

        //dfs called recursively
        private void DFS(int row, int col,ref long score,bool[,] visited,bool countUniquePaths)
        {
            visited[row,col] = true;
            // System.Console.WriteLine($"{row} {col}   value {input[row][col]}");
            if (input[row][col] == 9)
            {
                score++;
            }

            foreach (int[] dir in directions)
            {
                int newRow = row + dir[0];
                int newCol = col + dir[1];

                if (newRow < 0 || newCol < 0 || newRow >= input.Length || newCol >= input[0].Length)
                    continue;
                if(!countUniquePaths)
                    if (visited[newRow,newCol])
                        continue;

                if (input[newRow][newCol] == input[row][col] + 1)
                {
                    DFS(newRow, newCol, ref score, visited,countUniquePaths);
                }
            }
        }
        public long Part1()
        {
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == 0)
                    {
                        long score = 0;
                        bool[,] visited = new bool[input.Length, input[0].Length];
                        DFS(i, j, ref score, visited,false);
                        result += score;
                    }
                }
            }
            return result;
        }

        public long Part2()
        {
            long result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == 0)
                    {
                        long score = 0;
                        bool[,] visited = new bool[input.Length, input[0].Length];
                        DFS(i, j, ref score, visited,true);
                        result += score;
                    }
                }
            }
            return result;
        }
    }
}
