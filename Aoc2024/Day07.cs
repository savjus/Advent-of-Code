namespace Aoc2024
{
    class Day07 : IAocDay
    {
        string[] lines;
        public Day07(string input)
        {
            lines = input.Split('\n');
            
        }
        public long Part1()
        {
            long ans = 0;
            foreach (string line in lines)
            {
                // Parse lines
                string[] sides = line.Split(':');
                long goal = long.Parse(sides[0]);
                List<long> values = sides[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => long.Parse(x))
                    .ToList();
                //check the operations
                if (CheckOperations(goal, values, values.Count - 1, false))
                    ans += goal;
            }
            return ans;
            //3245122495150
        }


        public long Part2()
        {
            long ans = 0;
            foreach (string line in lines)
            {
                string[] sides = line.Split(':');
                long goal = long.Parse(sides[0]);
                List<long> values = sides[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => long.Parse(x))
                    .ToList();
                //check the operations
                if (CheckOperations(goal, values, values.Count - 1, true))
                    ans += goal;
            }
            return ans;
            //105517128211543
        }
        

         /// <summary>
        /// recursive backtracking check, if list of numbers can add or multiply to get given num
        /// </summary>
        /// <param name="goalNum"></param>
        /// <param name="givenNums"></param>
        /// <returns></returns>
        private bool CheckOperations(long goalNum, List<long> givenNums, int position,bool allowConcatenation)
        {
            if (position == 0)
                return goalNum == givenNums[0];
            //check by subtracting and dividing for speed 
            //subtract
            if (goalNum > givenNums[position] &&
                CheckOperations(goalNum - givenNums[position], givenNums, position - 1,allowConcatenation))
                return true;
            //divide
            if (goalNum % givenNums[position] == 0 &&
                CheckOperations(goalNum / givenNums[position], givenNums, position - 1,allowConcatenation))
                return true;

            //concatenate  
            if (allowConcatenation)
            {
                // 10 with same number of digits as goalNum
                long divideBy = (long)Math.Pow(10, Math.Floor(Math.Log10(givenNums[position])) + 1);
                if (goalNum % divideBy == givenNums[position] &&
                CheckOperations(goalNum / divideBy, givenNums, position - 1, allowConcatenation))
                    return true;
            }
            return false;
        }
    }
}
