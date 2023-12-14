namespace advent_of_code_2023.Day9
{
    public class Day09
    {
        public static void SolvePart1()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day9/input.txt");

            var sequences = input.Select(line => line.Split(" ").Select(int.Parse).ToList());

            foreach (var sequence in sequences)
            {
                var deltas = new Stack<List<int>>();
                deltas.Push(sequence);

                while (deltas.Peek().Any())
                {
                    deltas.Push(deltas.Peek().Skip(1).Zip(deltas.Peek(), (first, second) => first - second).ToList());
                }

                deltas.Pop();

                while (deltas.Count > 0)
                {
                    var pop = deltas.Pop();
                    output += pop[^1];
                }

            }

            Console.WriteLine($"Result of Day 9 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day9/input.txt");

            var sequences = input.Select(line => line.Split(" ").Select(int.Parse).ToList());

            foreach (var sequence in sequences)
            {
                var deltas = new Stack<List<int>>();
                deltas.Push(sequence);

                while (deltas.Peek().Any())
                {
                    deltas.Push(deltas.Peek().Skip(1).Zip(deltas.Peek(), (first, second) => first - second).ToList());
                }

                deltas.Pop();
                var first = 0;

                // exactly the same only that we have to use diff and first value
                while (deltas.Count > 0)
                {
                    var pop = deltas.Pop();
                    first = pop[0] - first;
                }

                output += first;
            }

            Console.WriteLine($"Result of Day 9 - Part 2 is {output}");
        }
    }
}