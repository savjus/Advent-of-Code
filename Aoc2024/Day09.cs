using System.Collections.ObjectModel;

namespace Aoc2024
{
    class Day09 : IAocDay
    {
        string Input;
        public Day09(string input)
        {
            Input = input;
        }
        // 12345 -> 0..111....22222 -> 022111222...... (move nums from right to left into empty space)
        // then count sum, by multiple by rising index. 0*0, 1*2, 2*2, 3*1...
        // if even -> add to front
        // if odd -> add to empty

        //              (figuring out how to math without loop through every digit)
        //      2222   would be 0*2 + 1*2 + 2*2 + 3*2 = 12  
        // we can know length = 4, number = 2, index = 0
        //   index -> n                         n,n+1,n+2,n+3

        public long Part1()
        {
            int[] nums = Input.ToCharArray().Select(c => c - '0').ToArray();
            long result = 0L;
            int k = 0; // result string front pointer
            int i = 0; //input number front pointer
            int j = nums.Length - 1; // input number back pointer
            while (true)
            {
                if (i % 2 == 0)
                {
                    result += UpdateFileCheckSum(nums[i], i / 2, ref k);
                    nums[i] = 0;
                    if (i >= j)
                        break;
                    i += 1;
                }
                else // empty space
                {
                    if (i >= j)
                    {
                        result += (nums[j] + 1) * nums[j] / 2;
                        break;
                    }
                    // moves one of the pointers
                    int m = Math.Min(nums[i], nums[j]);
                    result += UpdateFileCheckSum(m, j / 2, ref k);
                    nums[i] -= m;
                    nums[j] -= m;
                    if (nums[i] == 0) //skips if front input num is 0
                        i += 1;
                    if (nums[j] == 0)// skips if last input num is 0;  
                        j -= 2;
                }
            }
            return result;
        }
        public long Part2()
        {
            int[] nums = Input.ToCharArray().Select(c => c - '0').ToArray();
            int[] numsCopy = nums.ToArray();
            long result = 0L;
            int k = 0;
            int i = 0;
            while (i < nums.Length)
            {
                if (i % 2 == 0)
                {
                    if (nums[i] == 0)
                        k += numsCopy[i];
                    result += UpdateFileCheckSum(nums[i], i / 2, ref k);
                    nums[i] = 0;
                    i += 1;
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
                        result += UpdateFileCheckSum(nums[j], j / 2, ref k);
                        nums[i] -= nums[j];
                        nums[j] = 0;
                        if (nums[i] == 0)
                            i += 1;
                    }
                    else
                    {
                        k += nums[i];
                        i += 1;
                    }
                }
            }
            return result;
        }

        // curr = number 
        // value = i/2
        // k is the beginning of the next number pointer, 2k is 
        private static long UpdateFileCheckSum(int cur, long value, ref int k)
        {
            long ret = (cur - 1 + 2 * k) * cur / 2 * value;
            k += cur;
            return ret;
        }
    }
}