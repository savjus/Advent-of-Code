using System.Collections.ObjectModel;

namespace Aoc2024
{
    class Day09 : IAocDay
    {
        int[] nums;
        public Day09(string input)
        {
            nums = input.Select(x => x - '0').ToArray(); // turn to ints
        }
        // 12345 -> 0..111....22222 -> 022111222...... (move nums from right to left into empty space)
        // then count sum, by multiple by rising index. 0*0, 1*2, 2*2, 3*1...
        // if even -> add to front
        // if odd -> add to empty

        //              (figuring out how to math without loop through every digit)
        //      2222   would be 0*2 + 1*2 + 2*2 + 3*2 = 12  
        // we can know length = 4, number = 2, index = 0
        //   index -> n                         n,n+1,n+2,n+3


        /// <summary>
        /// 
        /// </summary>
        /// <param name="length">length of same value number group</param>
        /// <param name="value">value of number</param>
        /// <param name="index">position start of the group</param>
        /// <returns></returns>
        private long CheckSum(int length, int value, ref int index)
        {
            long sum = (length-1 + 2 * index) * length / 2 * value;
            index += length;
            return sum;
        }


        public long Part1()
        {
            long result = 0;
            int i = 0;
            int k = 0;
            int j = nums.Length -1;
            while (true)
            {
                if (nums[i] % 2 == 0)
                {
                    result += CheckSum(nums[i], i / 2, ref k);
                    nums[i] = 0;
                    if (i >= j)
                        break;
                    i++;
                }
                else
                {
                    int m = Math.Min(nums[i], nums[j]);
                    result += CheckSum(m, j / 2, ref k);
                    nums[i] -= m;
                    nums[j] -= m;
                    if (nums[i] == 0)
                        i++;
                    if (nums[j] == 0)
                        j = j - 2;
                }
                break;
            }
            return result;
        }
        public long Part2()
        {
            int[] numsCopy = nums;
            int k = 0;
            int i = 0;
            long result = 0;
            while (i < nums.Length)
            {
                if (i % 2 == 0)
                {
                    if (nums[i] == 0)
                        k += numsCopy[i];
                    result += CheckSum(nums[i], i / 2, ref k);
                    nums[i] = 0;
                    i++;
                }
                else
                {
                    bool found = false;
                    int j = -1;
                    var loopTo = i;
                    for (j = nums.Length - 1; j >= loopTo; j -= 2)
                    {
                        if (nums[j] > 0 && nums[j] <= nums[i])
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        result += CheckSum(nums[j], j / 2, ref k);
                        nums[i] -= nums[j];
                        nums[j] = 0;
                        if (nums[i] == 0)
                            i++;
                    }
                    else
                    {
                        k += nums[i];
                        i++;
                    }
                }
            }
            return result;
        }
    }
}
