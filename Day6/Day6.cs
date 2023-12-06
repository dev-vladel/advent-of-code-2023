using System.Text.RegularExpressions;

namespace advent_of_code_2023.Day6
{
    public class Day6
    {
        public static void SolvePart1()
        {
            var outputQuadratic = 1;
            var outputBruteForce = 1;
            var input = File.ReadAllLines("../../../Day6/input.txt");

            //solution
            var timeLine = input[0];
            var distanceLine = input[1];

            var timeInfo = new TimeInfo() { Times = timeLine.Split(':')[1].Split(' ').Where(t => !string.IsNullOrEmpty(t)).Select(int.Parse).ToList() };
            var distanceInfo = new DistanceInfo() { Distances = distanceLine.Split(':')[1].Split(' ').Where(t => !string.IsNullOrEmpty(t)).Select(int.Parse).ToList() };

            var pairs = timeInfo.Times.Zip(distanceInfo.Distances);

            foreach (var pair in pairs)
            {
                // Quadratic (fails for example input)
                var discriminant = Math.Sqrt(pair.First * pair.First - 4 * pair.Second);
                var numberOfWays = Math.Floor((pair.First + discriminant) / 2) - Math.Ceiling((pair.First - discriminant) / 2) + 1;

                outputQuadratic *= (int)numberOfWays;

                // Brute Force (works flawlessly)
                var recordBeaten = 0;

                for (int i = 0; i <= pair.First; i++)
                {
                    var distanceTraveled = i * pair.First - i * i;

                    if (distanceTraveled > pair.Second)
                    {
                        recordBeaten++;
                    }
                }

                outputBruteForce *= recordBeaten;
            }

            Console.WriteLine($"Result of Day 6 - Part 1 (quadratic) is {outputQuadratic}");
            Console.WriteLine($"Result of Day 6 - Part 1 (brute force) is {outputBruteForce}");
        }

        public static void SolvePart2()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day6/testInput.txt");

            //solution

            Console.WriteLine($"Result of Day 6 - Part 2 is {output}");
        }

        private class TimeInfo
        {
            public List<int> Times {  get; set; } = new List<int>();
        }

        private class DistanceInfo
        {
            public List<int> Distances { get; set; } = new List<int>();
        }
    }
}