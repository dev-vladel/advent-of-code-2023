namespace advent_of_code_2023.Day4
{
    public class Day4
    {
        public static void SolvePart1()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day4/input.txt");

            //solution
            var winningNumbers = new List<int>();
            var availableNumbers = new List<int>();

            foreach (var line in input)
            {
                var points = 0;

                var split = line.Split('|');
                winningNumbers = split[0].Split(':')[1].Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
                availableNumbers = split[1].Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();

                var matchingNumbers = availableNumbers.Intersect(winningNumbers).ToList();

                foreach (var match in matchingNumbers)
                {
                    points = points == 0 ? 1 : points * 2;
                }

                output += points;
            }


            Console.WriteLine($"Result of Day 4 - Part 1 is {output}");
        }
        
        public static void SolvePart2()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day4/testInput.txt");

            //solution
            List<Card> cards = new List<Card>();

            foreach (var line in input)
            {
                var card = new Card();

                var split = line.Split('|');

                card.Id = int.Parse(split[0].Split(':')[0].Split(' ')[1]);
                card.WinningNumbers = split[0].Split(':')[1].Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();
                card.AvailableNumbers = split[1].Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(int.Parse).ToList();

                cards.Add(card);
            }

            var numberOfCards = new int[cards.Count];

            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                numberOfCards[i] += 1;

                var matchingNumbers = card.WinningNumbers.Intersect(card.AvailableNumbers).Count();

                if (matchingNumbers > 0)
                {
                    for (int j = 0, idToIncrease = i + 1; j < matchingNumbers && idToIncrease <= cards.Count; j++)
                    {
                        numberOfCards[idToIncrease++] += numberOfCards[i];
                    }
                }
            }

            foreach (var number in numberOfCards)
            {
                output += number;
            }

            Console.WriteLine($"Result of Day 4 - Part 2 is {output}");
        }

        private class Card
        {
            public int Id { get; set; }
            public List<int> WinningNumbers { get; set; } = new List<int>();
            public List<int> AvailableNumbers { get; set; } = new List<int>();
            public int NumberOfOccurences { get; set; } = 0;
        }
    }
}