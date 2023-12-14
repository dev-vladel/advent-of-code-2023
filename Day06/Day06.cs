using System.Text.RegularExpressions;

namespace advent_of_code_2023.Day6
{
    public class Day06
    {
        public static void SolvePart1()
        {
            double outputQuadratic = 1;
            double outputBruteForce = 1;
            var input = File.ReadAllLines("../../../Day6/input.txt");

            //solution
            var timeLine = input[0];
            var distanceLine = input[1];

            var timeInfo = new TimeInfo() { Times = timeLine.Split(':')[1].Split(' ').Where(t => !string.IsNullOrEmpty(t)).Select(double.Parse).ToList() };
            var distanceInfo = new DistanceInfo() { Distances = distanceLine.Split(':')[1].Split(' ').Where(t => !string.IsNullOrEmpty(t)).Select(double.Parse).ToList() };

            var pairs = timeInfo.Times.Zip(distanceInfo.Distances);

            foreach (var pair in pairs)
            {
                // Quadratic (fails for example input)
                var discriminant = Math.Sqrt(pair.First * pair.First - 4 * pair.Second);
                var numberOfWays = Math.Floor((pair.First + discriminant) / 2) - Math.Ceiling((pair.First - discriminant) / 2) + 1;

                outputQuadratic *= numberOfWays;

                // Brute Force (works flawlessly)
                var recordBeaten = 0;

                for (long i = 0; i <= pair.First; i++)
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
            double outputQuadratic = 1;
            double outputBruteForce = 1;
            var input = File.ReadAllLines("../../../Day6/input.txt");

            //solution
            var timeLine = input[0];
            var distanceLine = input[1];

            var timeInfo = double.Parse(timeLine.Split(':')[1].Replace(" ", ""));
            var distanceInfo = double.Parse(distanceLine.Split(':')[1].Replace(" ", ""));

            // Quadratic (fails for example input)
            var discriminant = Math.Sqrt(timeInfo * timeInfo - 4 * distanceInfo);
            var numberOfWays = Math.Floor((timeInfo + discriminant) / 2) - Math.Ceiling((timeInfo - discriminant) / 2) + 1;

            outputQuadratic *= numberOfWays;

            // Brute Force (works flawlessly)
            var recordBeaten = 0;

            for (long i = 0; i <= timeInfo; i++)
            {
                var distanceTraveled = i * timeInfo - i * i;

                if (distanceTraveled > distanceInfo)
                {
                    recordBeaten++;
                }
            }

            outputBruteForce *= recordBeaten;

            Console.WriteLine($"Result of Day 6 - Part 2 (quadratic) is {outputQuadratic}");
            Console.WriteLine($"Result of Day 6 - Part 2 (brute force) is {outputBruteForce}");
        }

        private class TimeInfo
        {
            public List<double> Times {  get; set; } = new List<double>();
        }

        private class DistanceInfo
        {
            public List<double> Distances { get; set; } = new List<double>();
        }
    }
}