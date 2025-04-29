using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2024
{
    class Day02 : IAocDay
    {
        private List<List<int>> nums;

        public Day02(string input)
        {
            nums = new List<List<int>>();
            string[] lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string line in lines)
            {
                List<int> ints = line
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                
                nums.Add(ints);
            }
        }

        public long Part1()
        {
            long result = 0;

            foreach (var item in nums)
            {
                if (item[0] > item[1])
                {
                    if (CheckDesending(item))
                        result++;
                }
                else
                {
                    if (CheckAscending(item))
                        result++;
                }
            }

            return result;
        }

        public long Part2()
        {
            long result = 0;
            foreach (var item in nums)
            {
                bool isValid = false;

                // First try without removing any numbers
                if (item[0] > item[1])
                {
                    if (CheckDesending(item))
                    {
                        isValid = true;
                    }
                }
                else
                {
                    if (CheckAscending(item))
                    {
                        isValid = true;
                    }
                }

                // If not valid, try removing one number at a time
                if (!isValid)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        var modifiedList = new List<int>(item);
                        modifiedList.RemoveAt(i);

                        if (modifiedList[0] > modifiedList[1])
                        {
                            if (CheckDesending(modifiedList))
                            {
                                isValid = true;
                                break;
                            }
                        }
                        else
                        {
                            if (CheckAscending(modifiedList))
                            {
                                isValid = true;
                                break;
                            }
                        }
                    }
                }

                if (isValid)
                {
                    result++;
                }
            }
            return result;
        }

        private bool CheckDesending(List<int> numbers, int allowedErrors = 1)
        {
            int count = 0;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] <= numbers[i + 1] || numbers[i] - numbers[i + 1] > 3)
                {
                    count++;
                    if (count >= allowedErrors)
                        return false;
                }
            }
            return true;
        }

        private bool CheckAscending(List<int> numbers, int allowedErrors = 1)
        {
            int count = 0;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (numbers[i] >= numbers[i + 1] || numbers[i + 1] - numbers[i] > 3)
                {
                    count++;
                    if (count >= allowedErrors)
                        return false;
                }
            }
            return true;
        }
    }
}
