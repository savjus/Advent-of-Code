namespace Aoc2024
{
    class Day05 : IAocDay
    {
        List<Rule> Rules;
        List<List<int>> Updates;

        long P2Answ;
        public Day05(string input)
        {
            Rules = new List<Rule>();
            Updates = new List<List<int>>();

            string[] lines = input.Split('\n');
            bool parsingRules = true;
            foreach (var line in lines)
            {
                if (parsingRules)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        parsingRules = false;
                    }
                    else
                    {
                        var numbers = line.Split('|').Select(int.Parse).ToArray();
                        Rules.Add(new Rule(numbers[0], numbers[1]));
                    }
                }
                else
                {
                    Updates.Add(line.Split(',').Select(int.Parse).ToList());
                }
            }
        }

        public long Part1()
        {
            long part1 = 0;
            long part2 = 0;

            foreach (var update in Updates) // cycles through nums 22,33,44....
            {
                var isValid = true;
                var matchingRules = new List<Rule>();

                foreach (var rule in Rules) // cycles through rules 5|3
                {
                    var indexLeft = update.IndexOf(rule.Left);
                    var indexRight = update.IndexOf(rule.Right);

                    if (indexLeft == -1 || indexRight == -1)
                    {
                        continue;
                    }

                    matchingRules.Add(rule); // adds the rule to the list

                    if (indexLeft > indexRight) // compares both numbers, tells if it breaks rule
                    {
                        isValid = false;
                    }
                }

                if (isValid) // if the numbers valid, adds the middle num
                {
                    part1 += update[update.Count / 2];
                }
                else // for part 2, if not valid checks for fix
                {
                    update.Sort((a, b) =>
                    {
                        var rule = matchingRules.Find(r => r.Contains(a) && r.Contains(b));

                        if (rule == default || rule.Left == a)
                        {
                            return -1;
                        }

                        return 1;
                    });

                    part2 += update[update.Count / 2];
                }
            }

            P2Answ = part2;
            return part1;
        }

        public long Part2()
        {
            return P2Answ;
        }
    }

    internal record Rule(int Left, int Right)
    {
        public bool Contains(int i) => Left == i || Right == i;
    }
}
