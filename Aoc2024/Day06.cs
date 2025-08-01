﻿namespace Aoc2024
{
    class Day06 : IAocDay
    {
        int _GuardRow { get; set; }
        int _GuardCol { get; set; }
        string[] lines;
        char[,] grid;
        string guard = ">v<^"; // Guard orientations: right, down, left, up

        // Dictionary to manage direction and movement based on orientation
        // {x,y,index} index for rotating to next index
        Dictionary<char, List<int>> direction = new Dictionary<char, List<int>>
        {
            {'>', new List<int> {1, 0, 0}},  // Right
            {'v', new List<int> {0, 1, 1}},  // Down
            {'<', new List<int> {-1, 0, 2}}, // Left
            {'^', new List<int> {0, -1, 3}}  // Up
        };


        public Day06(string input)
        {
            lines = input.Split('\n');
            // Search for the initial position of the guardcol and guardrow
            foreach (string line in lines)
            {
                _GuardCol = line.IndexOfAny(guard.ToCharArray());
                if (_GuardCol >= 0)
                {
                    break;
                }
                _GuardRow++;
            }

            grid = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }
        }

        public long Part1()
        {
            int limitRow = lines.Length;
            int limitCol = lines[0].Length;
            // Guard's current orientation
            int guardRow = _GuardRow;
            int guardCol = _GuardCol;
            char guardOrientation = lines[guardRow][guardCol];
            // Get the movement for the current orientation
            List<int> move = direction[guardOrientation];
            int x = move[0];
            int y = move[1];
            int index = move[2];
            // To track visited positions and prevent revisiting
            HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>(); 
            int count = 0;

            // Add initial position
            visitedPositions.Add((guardRow, guardCol));

            // While the guard is within the grid bounds, move
            while (guardRow + y >= 0 &&
                    guardRow + y < limitRow &&
                    guardCol + x >= 0 &&
                    guardCol + x < limitCol)
            {
                // Check if next space is an obstacle
                if (lines[guardRow + y][guardCol + x] == '#')
                {
                    // If obstacle is hit, turn 90 degrees to the right
                    index = (index + 1) % 4;
                    guardOrientation = guard[index];
                    move = direction[guardOrientation];
                    x = move[0];
                    y = move[1];
                    index = move[2];
                }
                else
                {
                    // Move the guard
                    guardRow += y;
                    guardCol += x;

                    // If the guard's new position is not visited yet, count it
                    if (!visitedPositions.Contains((guardRow, guardCol)))
                    {
                        visitedPositions.Add((guardRow, guardCol));
                        count++;
                    }
                }
            }
            return visitedPositions.Count();
        }

        public long Part2()
        {
            // Function to simulate the movement of the guard and check for loops
            int limitRow = lines.Length;    
            int limitCol = lines[0].Length;
            int guardRow = _GuardRow;
            int guardCol = _GuardCol;

            char guardOrientation = lines[guardRow][guardCol];
            // Try placing an obstruction at every empty position and check for loops
            int validPositionsCount = 0;
            for (int i = 0; i < limitRow; i++)
            {
                for (int j = 0; j < limitCol; j++)
                {
                    if (grid[i, j] == '.')
                    {
                        grid[i, j] = '#'; // Place an obstruction
                        if (CheckForLoop())
                        {
                            validPositionsCount++;
                        }
                        grid[i, j] = '.'; // Remove the obstruction and check next
                    }
                }
            }
            return validPositionsCount;//2143


            bool CheckForLoop()
            {
                HashSet<(int, int, char)> visitedPositions = new HashSet<(int, int, char)>();
                // To track visited positions with direction
                int row = guardRow;
                int col = guardCol;
                char orientation = guardOrientation;
                // Add the initial position with orientation to visited positions
                visitedPositions.Add((row, col, orientation));
                // Simulate the guard's movement
                while (true)
                {
                    // Check if the next move is valid and not out of bounds
                    int newRow = row + direction[orientation][1];
                    int newCol = col + direction[orientation][0];
                    if (newRow < 0 ||
                        newRow >= limitRow ||
                        newCol < 0 ||
                        newCol >= limitCol)
                          return false; // Out of bounds, no loop found
                    // Check if the next space is an obstacle
                    if (grid[newRow, newCol] == '#') // If obstacle is hit, turn 90 degrees to the right
                        orientation = guard[(guard.IndexOf(orientation) + 1) % 4];
                    else
                    {
                        // Move to the next position
                        row = newRow;
                        col = newCol;
                        // Check if the new position has been visited before (loop detection)
                        if (visitedPositions.Contains((row, col, orientation)))
                            return true; // A loop is found
                        // Add the new position with orientation to visited positions
                        visitedPositions.Add((row, col, orientation));
                    }
                }
            }
        }
    }
}

