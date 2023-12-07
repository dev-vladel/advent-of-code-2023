namespace advent_of_code_2023.Day7
{
    public static class Constants
    {
        public static readonly Dictionary<string, int> CardValues = new Dictionary<string, int>
        {
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"7", 7},
            {"8", 8},
            {"9", 9},
            {"T", 10},
            {"J", 11},
            {"Q", 12},
            {"K", 13},
            {"A", 14}
        };

        public static readonly Dictionary<string, int> CardValuesJoker = new Dictionary<string, int>
        {
            {"J", 1},
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"7", 7},
            {"8", 8},
            {"9", 9},
            {"T", 10},
            {"Q", 12},
            {"K", 13},
            {"A", 14}
        };

        public enum HandType
        {
            HighCard = 0,
            OnePair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKind = 6
        }
    }
}