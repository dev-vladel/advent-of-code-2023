using System.Xml.Linq;

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


        // Thanks to https://www.reddit.com/user/Salad-Extension/
        // and his https://www.reddit.com/r/adventofcode/comments/18df7px/comment/kchwt4l/?utm_source=share&utm_medium=web2x&context=3
        // Still trying to understand this concept/functions/math/what you want to call it

        public static void SolvePart2()
        {
            double output = 0;
            var input = File.ReadAllLines("../../../Day8/input.txt");

            var instructions = input[0].Select(x => x == 'L' ? 0 : 1).ToArray();
            //var elements = input.Skip(2).Select(e =>
            //{
            //    var split = e.Split("=")[1].Split(",");
            //    var leftSide = split[0].Trim().Substring(1);
            //    var rightSide = split[1].Trim().Substring(0, 3);

            //    return (Key: e.Split("=")[0].Trim(), LeftSide: leftSide, RightSide: rightSide);
            //}).ToDictionary(d => d.Key, d => (d.Key, d.LeftSide, d.RightSide));

            var elements = input.Skip(2)
                .Select(x => x.Split(new[] { ' ', ',', '(', ')', '=' }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(x => x[0], x => x[1..]);

            //var instructionIdentifier = Constants.StartingPositionShort;
            //var currentElements = elements.Values.Where(e => e.Key[2].Equals(instructionIdentifier)).ToList();

            //var frequency = new Dictionary<int, List<int>>();

            //while (true)
            //{
            //    foreach (var instruction in instructions)
            //    {
            //        currentElements = currentElements.Select(e => elements[instruction.Equals(Constants.InstructionLeft) ? e.LeftSide : e.RightSide]).ToList();
            //        output++;
            //    }
            //}

            var findloopFrequency = (string element) =>  // Scan until an end node is seen twice, first index is phase, index difference is period
            {
                var endSeen = new Dictionary<string, long>();

                for (long i = 0; true; i++)
                {
                    if (element[2] == 'Z')
                    {
                        if (endSeen.TryGetValue(element, out var lastSeen))
                        {
                            return (phase: lastSeen, period: i - lastSeen);
                        }
                        else
                        {
                            endSeen[element] = i;
                        }
                    }

                    element = elements[element][instructions[i % instructions.Length]];
                }
            };

            var frequencies =
                elements.Keys
                .Where(x => x[2] == 'A')
                .Select(x => findloopFrequency(x))
                .ToList();

            // Find harmony by moving harmony phase forward and increasing harmony period until it matches all frequencies
            var harmonyPhase = frequencies[0].phase;
            var harmonyPeriod = frequencies[0].period;
            foreach (var freq in frequencies.Skip(1))
            {
                // Find new harmonyPhase by increasing phase in harmony period steps until harmony matches freq
                for (; harmonyPhase < freq.phase || (harmonyPhase - freq.phase) % freq.period != 0; harmonyPhase += harmonyPeriod) ;

                // Find the new harmonyPeriod by looking for the next position the harmony frequency matches freq (brute force least common multiplier)
                long sample = harmonyPhase + harmonyPeriod;
                for (; (sample - freq.phase) % freq.period != 0; sample += harmonyPeriod) ;
                harmonyPeriod = sample - harmonyPhase;
            }

            output = harmonyPeriod;

            // solution

            Console.WriteLine($"Result of Day 8 - Part 2 is {output}");
        }
    }
}