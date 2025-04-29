using System.Collections.Immutable;
using System.Data.Common;
using System.Xml.Linq;

namespace Aoc2023;

public class Day07 : IAocDay
{
    private readonly string[] lines;
    private readonly Dictionary<string, int> handRanks = new Dictionary<string, int>
    {
        { "High card", 1 },
        { "Pair", 2 },
        { "Two pair", 3 },
        { "Three of a kind", 4 },
        { "Full house", 5 },
        { "Four of a kind", 6 },
        { "Five of a kind", 7 },
    };
    
    public Day07(string input)
    {
        lines = input.Split('\n');
    }

    public long Part1()
    {
        string cards = "23456789TJQKA";//higher index = better card
        

        List<(string hand, string type , long value)> hands = new List<(string, string, long)>();
        for (int i = 0; i < lines.Length; i++)
        {
            string [] parts = lines[i].Split(' ');
            string hand = parts[0];
            long value = long.Parse(parts[1]);
            //identify the type of hand and add it to the dictionary.
            string type = IdentifyCardType(hand);
            hands.Add((hand,type, value));
        }
        //assign ranks to every hand
        //worst hand gets lowest rank
        //if hand type are the same then order by checking the cards from 1 to 5
        hands.Sort((a, b) =>
        {
            //sort by types
            int compare = handRanks[a.type].CompareTo(handRanks[b.type]);
            if (compare != 0) return compare;
            
            //sort by individual card ranks
            for (int i = 0; i < 5; i++)
            {
                int hand1 = cards.IndexOf(a.hand[i]);
                int hand2 = cards.IndexOf(b.hand[i]);
                if (hand1 != hand2) return hand1.CompareTo(hand2);
            }

            return 0;
        });
        long ans = 0;
        for(int i = 0;i<hands.Count;i++)
        {
           // Console.WriteLine($"{hands[i].hand} {hands[i].type} {hands[i].value}");
            ans += (i + 1) * hands[i].value;
        }
        return ans;
    }
    
    //254114074 high
    public long Part2()
    {
        string cards = "J23456789TQKA";
        List<(string hand, string type , long value)> hands = new List<(string, string, long)>();
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(' ');
            string hand = parts[0];
            string type = "";
            type = IdentifyCardTypeWildCard(hand);
            hands.Add((hand,type, long.Parse(parts[1])));
        }
        hands.Sort((a, b) =>
        {
            //sort by types
            int compare = handRanks[a.type].CompareTo(handRanks[b.type]);
            if (compare != 0) return compare;
            
            //sort by individual card ranks
            for (int i = 0; i < 5; i++)
            {
                int hand1 = cards.IndexOf(a.hand[i]);
                int hand2 = cards.IndexOf(b.hand[i]);
                if (hand1 != hand2) return hand1.CompareTo(hand2);
            }

            return 0;
        });
        long ans = 0;
        for(int i = 0;i<hands.Count;i++)
        {
            //Console.WriteLine($"{hands[i].hand} {hands[i].type} {hands[i].value}");
            ans += (i + 1) * hands[i].value;
        }
        return ans;
    }
    public string IdentifyCardTypeWildCard(string hand)
    {
        Dictionary<char, int> count = new Dictionary<char, int>();
        foreach (var card in hand)
        {
            if (!count.TryAdd(card, 1))
            {
                count[card]++;
            }
        }

        int wild = count.ContainsKey('J') ? count['J'] : 0;
        if (wild == 5) return "Five of a kind"; // All Js case

        // Remove 'J' from dictionary to get the strongest remaining card
        if (wild > 0) count.Remove('J');

        // Find the most frequent non-J card count
        char bestCard = count.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        count[bestCard] += wild;

        int maxCount = count.Values.Max();

        switch (maxCount)
        {
            case 5: return "Five of a kind";
            case 4: return "Four of a kind";
            case 3: return count.Values.Count(v => v == 2) == 1 ? "Full house" : "Three of a kind";
            case 2: return count.Values.Count(v => v == 2) == 2 ? "Two pair" : "Pair";
            default: return "High card";
        }
    }



    public string IdentifyCardType(string hand)
    {
        Dictionary<char, int> similarCount = new Dictionary<char, int>();
        foreach (var card in hand)
            if (!similarCount.TryAdd(card, 1))
                similarCount[card] += 1;

        switch (similarCount.Values.Max())
        {
            case 2: return (similarCount.Values.Count(v => v == 2) == 2) ? "Two pair" : "Pair";
            case 3: return (similarCount.Values.Contains(2)) ? "Full house" : "Three of a kind";
            case 4: return "Four of a kind";
            case 5: return "Five of a kind";
            default: return "High card";
        }
    }
}
