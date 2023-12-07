namespace advent_of_code_2023.Day7
{
    public class Day7
    {
        public static void SolvePart1()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day7/input.txt");

            var hands = new List<Hand>();

            //solution
            foreach (var line in input)
            {
                var split = line.Split(' ');

                var hand = new Hand()
                {
                    Label = split[0],
                    LabelTypes = split[0].Distinct().ToList(),
                    Bid = int.Parse(split[1])
                };

                foreach (var character in hand.LabelTypes)
                {
                    var count = hand.Label.Count(l => l == character);
                    hand.Strength[character.ToString()] = count;
                }

                var maxStrength = hand.Strength.Values.Max();

                switch (maxStrength)
                {
                    // High Card
                    case 1:
                        hand.TypeOfPair = 0;
                        break;

                    // One pair or Two Pair
                    case 2:
                        hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);
                        hand.TypeOfPair = hand.Strength.Values.Max() == 1 ? 1 : 2;

                        break;

                    // Three of a Kind or Full House
                    case 3:
                        hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);
                        hand.TypeOfPair = hand.Strength.Values.Max() == 2 ? 4 : 3;

                        break;

                    // Four of a Kind
                    case 4:
                        hand.TypeOfPair = 5;
                        break;

                    // Five of a Kind
                    case 5:
                        hand.TypeOfPair = 6;
                        break;
                }

                hands.Add(hand);
            }

            var orderedHand = new List<Hand>();

            for (int i = 0; i <= 6; i++)
            {
                var pairList = hands.Where(h => h.TypeOfPair == i).ToList();

                for (int j = 0; j < pairList.Count - 1; j++)
                {
                    for (int h = j + 1; h < pairList.Count; h++)
                    {
                        if (VerifyStrength(pairList[j], pairList[h]))
                        {
                            (pairList[h], pairList[j]) = (pairList[j], pairList[h]);
                        }
                    }
                }

                orderedHand.AddRange(pairList);
            }

            foreach (var hand in orderedHand)
            {
                output += hand.Bid * (orderedHand.IndexOf(hand) + 1);
            }

            Console.WriteLine($"Result of Day 7 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day7/input.txt");

            var hands = new List<Hand>();

            //solution
            foreach (var line in input)
            {
                var split = line.Split(' ');

                var hand = new Hand()
                {
                    Label = split[0],
                    LabelTypes = split[0].Distinct().ToList(),
                    Bid = int.Parse(split[1])
                };

                foreach (var character in hand.LabelTypes)
                {
                    var count = hand.Label.Count(l => l == character);
                    hand.Strength[character.ToString()] = count;
                }

                var maxStrength = hand.Strength.Values.Max();

                switch (maxStrength)
                {
                    // High Card
                    case 1:
                        // If Joker present, best case is to have a One Pair
                        hand.TypeOfPair = hand.LabelTypes.Contains('J') ? 1 : 0;

                        break;

                    // One Pair or Two Pair
                    case 2:
                        // If Joker present, new cases can be Three of a Kind or Full House
                        if (hand.LabelTypes.Contains('J'))
                        {
                            var strength = hand.Strength.FirstOrDefault(h => h.Value == maxStrength);

                            if (strength.Key == "J") 
                            {
                                hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);

                                if (hand.Strength.Values.Max() == 1)
                                {
                                    hand.TypeOfPair = 3;
                                }
                                else if (hand.Strength.Values.Max() == 2)
                                {
                                    hand.TypeOfPair = 5;
                                }
                            }
                            else
                            {
                                hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);

                                if (hand.Strength.Values.Max() == 2)
                                {
                                    hand.TypeOfPair = 4;
                                }
                                else
                                {
                                    hand.TypeOfPair = 3;
                                }
                            }
                        }
                        else
                        {
                            hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);
                            hand.TypeOfPair = hand.Strength.Values.Max() == 2 ? 2 : 1;
                        }

                        break;

                    // Three of a Kind or Full House
                    case 3:
                        // If Joker present, new cases can be Full House
                        if (hand.LabelTypes.Contains('J'))
                        {
                            var strength = hand.Strength.FirstOrDefault(h => h.Value == maxStrength);

                            if (strength.Key == "J")
                            {
                                hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);

                                if (hand.Strength.Values.Max() == 2)
                                {
                                    hand.TypeOfPair = 6;
                                }
                                else if (hand.Strength.Values.Max() == 1)
                                {
                                    hand.TypeOfPair = 5;
                                }
                            }
                            else
                            {
                                hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);

                                if (hand.Strength.Values.Max() == 2)
                                {
                                    hand.TypeOfPair = 6;
                                }
                                else if (hand.Strength.Values.Max() == 1)
                                {
                                    hand.TypeOfPair = 5;
                                }
                            }
                        }
                        else
                        {
                            hand.Strength.Remove(hand.Strength.FirstOrDefault(h => h.Value == maxStrength).Key);
                            hand.TypeOfPair = hand.Strength.Values.Max() == 2 ? 4 : 3;
                        }

                        break;

                    // Four of a Kind
                    case 4:
                        // If Joker present, best case is to have a Five of a Kind
                        hand.TypeOfPair = hand.LabelTypes.Contains('J') ? 6 : 5;

                        break;

                    // Five of a Kind
                    case 5:
                        hand.TypeOfPair = 6;
                        break;
                }

                hands.Add(hand);
            }

            var orderedHand = new List<Hand>();

            for (int i = 0; i <= 6; i++)
            {
                var pairList = hands.Where(h => h.TypeOfPair == i).ToList();

                for (int j = 0; j < pairList.Count - 1; j++)
                {
                    for (int h = j + 1; h < pairList.Count; h++)
                    {
                        if (VerifyStrengthTwo(pairList[j], pairList[h]))
                        {
                            (pairList[h], pairList[j]) = (pairList[j], pairList[h]);
                        }
                    }
                }

                orderedHand.AddRange(pairList);
            }

            foreach (var hand in orderedHand)
            {
                output += hand.Bid * (orderedHand.IndexOf(hand) + 1);
            }

            Console.WriteLine($"Result of Day 7 - Part 2 is {output}");
        }

        private class Hand
        {
            public int TypeOfPair { get; set; } = 0;
            public List<char> LabelTypes { get; set; } = new List<char>();
            public string Label { get; set; } = string.Empty;
            public int Bid { get; set; } = 0;

            public Dictionary<string, int> Strength = new()
            {
                {"2", 0},
                {"3", 0},
                {"4", 0},
                {"5", 0},
                {"6", 0},
                {"7", 0},
                {"8", 0},
                {"9", 0},
                {"T", 0},
                {"J", 0},
                {"Q", 0},
                {"K", 0},
                {"A", 0}
            };
        }

        private static bool VerifyStrength(Hand one, Hand two)
        {
            var result = false;

            for (int i = 0; i < one.Label.Length; i++)
            {
                if (one.Label[i] == two.Label[i])
                {
                    continue;
                }
                else
                {
                    result = Constants.CardValues[one.Label[i].ToString()] > Constants.CardValues[two.Label[i].ToString()];

                    break;
                }
            }

            return result;
        }

        private static bool VerifyStrengthTwo(Hand one, Hand two)
        {
            var result = false;

            for (int i = 0; i < one.Label.Length; i++)
            {
                if (one.Label[i] == two.Label[i])
                {
                    continue;
                }
                else
                {
                    result = Constants.CardValuesWithWeakerJ[one.Label[i].ToString()] > Constants.CardValuesWithWeakerJ[two.Label[i].ToString()];

                    break;
                }
            }

            return result;
        }
    }
}