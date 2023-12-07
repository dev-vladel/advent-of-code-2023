using static advent_of_code_2023.Day7.Constants;

namespace advent_of_code_2023.Day7
{
    public class Day7
    {
        public static void SolvePart1()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day7/input.txt");

            var hands = new List<Hand>();

            foreach (var line in input)
            {
                var split = line.Split(' ');

                var hand = new Hand(split[0], long.Parse(split[1]));

                hands.Add(hand);
            }

            var comparer = new HandComparer();
            hands.Sort(comparer);

            foreach (var hand in hands)
            {
                output += hand.Bid * (hands.IndexOf(hand) + 1);
            }

            Console.WriteLine($"Result of Day 7 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day7/input.txt");

            var hands = new List<Hand>();

            foreach (var line in input)
            {
                var split = line.Split(' ');

                var hand = new Hand(split[0], long.Parse(split[1]), true);

                hands.Add(hand);
            }

            var comparer = new HandComparer();
            hands.Sort(comparer);

            foreach (var hand in hands)
            {
                output += hand.Bid * (hands.IndexOf(hand) + 1);
            }

            Console.WriteLine($"Result of Day 7 - Part 1 is {output}");
        }

        public class Hand
        {
            public List<int> Cards { get; set; } = new List<int>();
            public long Bid { get; set; } = 0;
            public HandType HandType { get; set; }

            public Hand(string cards, long bid, bool hasJokers = false)
            {
                Bid = bid;

                foreach (var card in cards)
                {
                    Cards.Add(GetCardValue(card, hasJokers));
                }

                HandType = hasJokers ? GetHandTypeJokers(Cards)  : GetHandType(Cards);
            }
        }

        public class HandComparer : Comparer<Hand>
        {
            public override int Compare(Hand? handOne, Hand? handTwo)
            {
                if (handOne == null || handTwo == null)
                {
                    return -1;
                }

                var typeOne = (int)handOne.HandType;
                var typeTwo = (int)handTwo.HandType;

                if (typeOne != typeTwo)
                {
                    return typeOne.CompareTo(typeTwo);
                }
                else
                {
                    var index = 0;

                    while (handOne.Cards[index] == handTwo.Cards[index] && index < 4)
                    {
                        index++;
                    }

                    return handOne.Cards[index].CompareTo(handTwo.Cards[index]);
                }
            }
        }

        private static HandType GetHandType(List<int> cards)
        {
            var cardTypes = GetCardTypes(cards);

            if (cardTypes.Values.Any(c => c == 5))
            {
                return HandType.FiveOfAKind;
            }

            if (cardTypes.Values.Any(c => c == 4))
            {
                return HandType.FourOfAKind;
            }

            if (cardTypes.Values.Any(c => c == 3) && cardTypes.Values.Any(c => c == 2))
            {
                return HandType.FullHouse;
            }

            if (cardTypes.Values.Any(c => c == 3))
            {
                return HandType.ThreeOfAKind;
            }

            if (cardTypes.Values.Count(c => c == 2) == 2)
            {
                return HandType.TwoPair;
            }

            if (cardTypes.Values.Any(c => c == 2))
            {
                return HandType.OnePair;
            }

            return HandType.HighCard;
        }

        private static HandType GetHandTypeJokers(List<int> cards)
        {
            var cardTypes = GetCardTypes(cards);

            var intermediateHandType = GetHandType(cards);

            if (!cardTypes.TryGetValue(1, out int cardCount))
            {
                return intermediateHandType;
            }

            if (intermediateHandType == HandType.FourOfAKind)
            {
                return HandType.FiveOfAKind;
            }
           
            if (intermediateHandType == HandType.FullHouse)
            {
                if (cardCount == 1)
                {
                    return HandType.FourOfAKind;
                }
                else
                {
                    return HandType.FiveOfAKind;
                }
            }

            if (intermediateHandType == HandType.ThreeOfAKind)
            {
                return HandType.FourOfAKind;
            }

            if (intermediateHandType == HandType.TwoPair)
            {
                if (cardCount == 1)
                {
                    return HandType.FullHouse;
                }
                else
                {
                    return HandType.FourOfAKind;
                }
            }

            if (intermediateHandType == HandType.OnePair)
            {
                return HandType.ThreeOfAKind;
            }

            if (intermediateHandType == HandType.HighCard)
            {
                return HandType.OnePair;
            }

            return intermediateHandType;
        }

        private static Dictionary<int, int> GetCardTypes(List<int> cards)
        {
            var cardTypes = new Dictionary<int, int>();

            foreach (var card in cards)
            {
                if (cardTypes.ContainsKey(card))
                {
                    cardTypes[card]++;
                }
                else
                {
                    cardTypes.Add(card, 1);
                }
            }

            return cardTypes;
        }

        private static int GetCardValue(char card, bool hasJokers)
        {
            return hasJokers ? CardValuesJoker[card.ToString()] : CardValues[card.ToString()];
        }
    }
}