namespace advent_of_code_2023.Day2
{
    public class Day2
    {
        public static void SolvePart1()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day2/input.txt");

            foreach (var line in input)
            {
                var lineSplit = line.Split(':').Select(s => s.Trim()).ToList();
                var gameId = lineSplit[0].Split(" ")[1];
                var cubeHands = lineSplit[1].Split(";").Select(s => s.Trim()).ToList();
                var possibleGame = true;

                foreach (var hand in cubeHands)
                {
                    var pairs = hand.Contains(",") ? hand.Split(",").Select(hand => hand.Trim()) : new[] { hand };

                    foreach (var pair in pairs)
                    {
                        var detail = pair.Split(" ");
                        var number = int.Parse(detail[0]);
                        var color = detail[1];

                        switch (color)
                        {
                            case "red":
                                possibleGame = possibleGame && number <= 12;
                                break;

                            case "blue":
                                possibleGame = possibleGame && number <= 14;
                                break;

                            case "green":
                                possibleGame = possibleGame && number <= 13;
                                break;
                        }
                    }
                }

                if (possibleGame)
                {
                    output += int.Parse(gameId);
                }
            }

            Console.WriteLine($"Result of Day 1 - Part 1 is {output}");

        }

        public static void SolvePart2()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day2/input.txt");

            foreach (var line in input)
            {
                var lineSplit = line.Split(':').Select(s => s.Trim()).ToList();
                var gameId = lineSplit[0].Split(" ")[1];
                var cubeHands = lineSplit[1].Split(";").Select(s => s.Trim()).ToList();

                var redMinimum = 0;
                var blueMinimum = 0;
                var greenMinimum = 0;

                foreach (var hand in cubeHands)
                {
                    var pairs = hand.Contains(",") ? hand.Split(",").Select(hand => hand.Trim()) : new[] { hand };

                    foreach (var pair in pairs)
                    {
                        var detail = pair.Split(" ");
                        var number = int.Parse(detail[0]);
                        var color = detail[1];

                        switch (color)
                        {
                            case "red":
                                redMinimum = redMinimum <= number ? number : redMinimum;
                                break;

                            case "blue":
                                blueMinimum = blueMinimum <= number ? number : blueMinimum;
                                break;

                            case "green":
                                greenMinimum = greenMinimum <= number ? number : greenMinimum;
                                break;
                        }
                    }
                }

                output += redMinimum * greenMinimum * blueMinimum;
            }

            Console.WriteLine($"Result of Day 1 - Part 1 is {output}");
            Console.WriteLine("---");
        }
    }
}