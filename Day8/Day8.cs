namespace advent_of_code_2023.Day8
{
    public class Day8
    {
        public static void SolvePart1()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day8/input.txt");

            var instructions = input[0];
            var elements = input.Skip(2).Select(e =>
            {
                var split = e.Split("=")[1].Split(",");
                var leftSide = split[0].Trim().Substring(1);
                var rightSide = split[1].Trim().Substring(0, 3);

                return (Key: e.Split("=")[0].Trim(), LeftSide: leftSide, RightSide: rightSide);
            }).ToDictionary(d => d.Key, d => (d.Key, d.LeftSide, d.RightSide));

            var instructionIdentifier = Constants.StartingPosition;
            var element = elements[instructionIdentifier];

            var finalPositionNotFound = true;

            while (finalPositionNotFound)
            {
                foreach (var instruction in instructions)
                {
                    instructionIdentifier = instruction.Equals(Constants.InstructionLeft)
                        ? element.LeftSide
                        : element.RightSide;

                    element = elements[instructionIdentifier];
                    output++;

                    if (element.Key.Equals(Constants.FinalPosition))
                    {
                        finalPositionNotFound = false;
                    }
                }
            }

            Console.WriteLine($"Result of Day 8 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day8/testInput.txt");

            // solution

            Console.WriteLine($"Result of Day 8 - Part 2 is {output}");
        }
    }
}