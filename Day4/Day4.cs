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

            Console.WriteLine($"Result of Day 4 - Part 2 is {output}");
        }
    }
}