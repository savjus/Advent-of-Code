namespace Aoc2024
{
    class Day09 : IAocDay
    {
        string line;
        int[] nums;
        public Day09(string input)
        {
            line = input;
            nums = input.Select(x => x - '0').ToArray();
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
        private long CheckSum(int length, int value, int index)
        {
            long sum = 0;
            return sum;
        }


        public long Part1()
        {
            long result = 0;
            return result;
        }
        public long Part2()
        {
            long result = 0;
            return result;
        }
    }
}
