using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc2023
{
    public class Day04 : IAocDay
    {
        private readonly int[][] winningNumbers; 
        private readonly int[][] gottenNumbers;
        private readonly int[] matchCounts;
        private readonly int numGames;
        
        public Day04(string input)
        {
            //12263631 p2
            string [] inputLines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            numGames = inputLines.Length;
            winningNumbers = new int [numGames][];
            gottenNumbers = new int [numGames][];
            matchCounts = new int [numGames];
            
            for (int i = 0; i < numGames; i++)
            {
                var sides = inputLines[i].Split(':')[1].Split('|');
                winningNumbers[i] = sides[0].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                gottenNumbers[i] = sides[1].Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                matchCounts[i] = winningNumbers[i].Intersect(gottenNumbers[i]).Count();
            }
        }
        public long Part1()
        {
            long score = 0;
            for (int i = 0; i < numGames; i++)
            {
                int matches = matchCounts[i];
                if (matches > 0)
                {
                    score += (1L << (matches - 1));
                }
            }
            return score;
        }
        public long Part2()
        {
            long[] cards = new long [numGames];
            Array.Fill(cards,1);
            for (int i = 0; i < numGames; i++)
            {
                int matches = matchCounts[i];
                long copies = cards[i];

                for (int j = i + 1; j <= i + matches && j < numGames; j++)
                {
                    cards[j] += copies;
                }
            }
            return cards.Sum();
        }
    }
}
