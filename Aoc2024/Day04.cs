using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2024
{
    class Day04 : IAocDay
    {
        string[] lines;
        public Day04(string input)
        {
            lines = input.Split('\n');
        }
        public long Part1()
        {
            long result = 0;
            result += hasXmas_UL_to_DR();
            result += hasXmas_DL_to_UR();
            result += hasXmasHorizontal();
            result += hasXmasVertical();
            return result;
        }
        public long Part2()
        {
            return hasMAS();
        }

        private long hasMAS()
        {
            long result = 0;
            for(int i = 0; i < lines.Length-2; i++)
            {
                for(int j = 0; j < lines[i].Length-2; j++)
                {
                    if(lines[i+1][j+1] == 'A')
                    {
                        if (lines[i][j] == 'M' && lines[i][j + 2] == 'M' && lines[i + 2][j] == 'S' && lines[i + 2][j + 2] == 'S' ||
                            lines[i][j] == 'S' && lines[i][j + 2] == 'S' && lines[i + 2][j] == 'M' && lines[i + 2][j + 2] == 'M' ||
                            lines[i][j] == 'S' && lines[i][j + 2] == 'M' && lines[i + 2][j] == 'S' && lines[i + 2][j + 2] == 'M' ||
                            lines[i][j] == 'M' && lines[i][j + 2] == 'S' && lines[i + 2][j] == 'M' && lines[i + 2][j + 2] == 'S') 
                        {
                                result++;

                        }
                    }
                }
            }
            return result;
        }


        private long hasXmas_DL_to_UR()
        {
            long result = 0;
            for (int i = 3; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length - 3; j++)
                {
                    if ((lines[i][j] == 'X' && lines[i - 1][j + 1] == 'M' && lines[i - 2][j + 2] == 'A' && lines[i - 3][j + 3] == 'S') ||
                        (lines[i][j] == 'S' && lines[i - 1][j + 1] == 'A' && lines[i - 2][j + 2] == 'M' && lines[i - 3][j + 3] == 'X'))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private long hasXmas_UL_to_DR()
        {
            long result = 0;
            for (int i = 0; i < lines.Length - 3; i++)
            {
                for (int j = 0; j < lines[i].Length - 3; j++)
                {
                    if ((lines[i][j] == 'X' && lines[i + 1][j + 1] == 'M' && lines[i + 2][j + 2] == 'A' && lines[i + 3][j + 3] == 'S') ||
                        (lines[i][j] == 'S' && lines[i + 1][j + 1] == 'A' && lines[i + 2][j + 2] == 'M' && lines[i + 3][j + 3] == 'X'))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private long hasXmasVertical()
        {
            long result = 0;
            for (int i = 0; i < lines.Length - 3; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if ((lines[i][j] == 'X' && lines[i + 1][j] == 'M' && lines[i + 2][j] == 'A' && lines[i + 3][j] == 'S') ||
                        (lines[i][j] == 'S' && lines[i + 1][j] == 'A' && lines[i + 2][j] == 'M' && lines[i + 3][j] == 'X'))
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        private long hasXmasHorizontal()
        {
            long result = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length - 3; j++)
                {
                    if ((lines[i][j] == 'X' && lines[i][j + 1] == 'M' && lines[i][j + 2] == 'A' && lines[i][j + 3] == 'S') ||
                        (lines[i][j] == 'S' && lines[i][j + 1] == 'A' && lines[i][j + 2] == 'M' && lines[i][j + 3] == 'X'))
                    {
                        result++;
                    }
                }
            }
            return result;
        }
    }
}
