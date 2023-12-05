namespace advent_of_code_2023.Day3
{
    public class Day3
    {
        public static void SolvePart1()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day3/input.txt");
            var newInput = RebuildInput(input);
            var newInputLength = newInput.Length;

            var indexLine = 1;

            // Traverse every line
            while (indexLine < newInputLength)
            {
                var number = string.Empty;
                var startOfNumberIndex = -1;
                var endofNumberIndex = -1;

                var lineCharArray = newInput[indexLine].ToCharArray();
                var indexColumn = 0;

                // Traverse every column in a line
                while (indexColumn < lineCharArray.Length)
                {
                    var character = lineCharArray[indexColumn];

                    if (char.IsNumber(character))
                    {
                        startOfNumberIndex = string.IsNullOrEmpty(number) ? indexColumn : startOfNumberIndex;
                        endofNumberIndex = indexColumn;

                        number = $"{number}{character}";
                    }
                    else if ((character == '.' || Constants.Symbols.Contains(character)) && !string.IsNullOrEmpty(number))
                    {
                        if (IsSymbolNearby(newInput[indexLine - 1], newInput[indexLine], newInput[indexLine + 1], startOfNumberIndex, endofNumberIndex))
                        {
                            output += int.Parse(number);
                        }

                        number = string.Empty;
                        startOfNumberIndex = -1;
                        endofNumberIndex = -1;
                    }

                    indexColumn++;
                }

                indexLine++;
            }

            Console.WriteLine($"Result of Day 3 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day3/input.txt");
            var newInput = RebuildInput(input);
            var newInputLength = newInput.Length;

            var indexLine = 1;
            var numbers = new List<Number>();

            // Traverse every line for finding numbers
            while (indexLine < newInputLength)
            {
                var number = string.Empty;
                var startOfNumberIndex = -1;
                var middleOfNumberIndex = -1;
                var endOfNumberIndex = -1;

                var lineCharArray = newInput[indexLine].ToCharArray();
                var indexColumn = 1;

                // Traverse every column in a line
                while (indexColumn < lineCharArray.Length)
                {
                    var character = lineCharArray[indexColumn];

                    if (char.IsNumber(character))
                    {
                        startOfNumberIndex = string.IsNullOrEmpty(number) ? indexColumn : startOfNumberIndex;
                        endOfNumberIndex = indexColumn;
                        middleOfNumberIndex = (int)Math.Ceiling((endOfNumberIndex + startOfNumberIndex) / 2f);

                        number = $"{number}{character}";
                    }
                    else if ((character == '.' || Constants.Symbols.Contains(character)) && !string.IsNullOrEmpty(number))
                    {
                        numbers.Add(new Number()
                        {
                            Value = int.Parse(number),
                            Line = newInput[indexLine],
                            FirstIndex = startOfNumberIndex,
                            MiddleIndex = middleOfNumberIndex,
                            LastIndex = endOfNumberIndex
                        });

                        number = string.Empty;
                        startOfNumberIndex = -1;
                        middleOfNumberIndex = -1;
                        endOfNumberIndex = -1;
                    }

                    indexColumn++;
                }

                indexLine++;
            }

            indexLine = 1;

            while (indexLine < newInputLength)
            {
                var lineCharArray = newInput[indexLine].ToCharArray();
                var indexColumn = 1;

                while (indexColumn < lineCharArray.Length)
                {
                    var character = lineCharArray[indexColumn];

                    if (character == '*')
                    {
                        output += GearRatio(numbers, newInput[indexLine - 1], newInput[indexLine], newInput[indexLine + 1], indexColumn);
                    }

                    indexColumn++;
                }

                indexLine++;
            }

            Console.WriteLine($"Result of Day 3 - Part 2 is {output}");
            Console.WriteLine("---");
        }

        /// <summary>
        /// Reads the original input and adds to it neutral values . (dot) for skipping later validations if the
        /// index is out of bounds or not.
        /// </summary>
        /// <param name="originalInput"></param>
        /// <returns>A same type of input but with neutral values around it.</returns>
        private static string[] RebuildInput(string[] originalInput)
        {
            // Number of rows
            var newInput = new string[originalInput.Length + 2];

            // Length of row
            var lineLength = originalInput[0].Length + 2;
            var lineIndex = 0;

            while (lineIndex < newInput.Length)
            {
                if (lineIndex == 0 || lineIndex == newInput.Length - 1)
                {
                    var rowIndex = 0;

                    while (rowIndex < lineLength)
                    {
                        newInput[lineIndex] = $"{newInput[lineIndex]}.";

                        rowIndex++;
                    }

                    lineIndex++;
                }
                else
                {
                    foreach (var line in originalInput)
                    {
                        newInput[lineIndex] = $".{line}.";
                        lineIndex++;
                    }
                }
            }

            return newInput;
        }

        /// <summary>
        /// Checks through the provided lines if they contain or not a valid symbol in the vicinity of the number's start and end index.
        /// </summary>
        /// <param name="currentLine"></param>
        /// <param name="previousLine"></param>
        /// <param name="nextLine"></param>
        /// <param name="startOfNumberIndex"></param>
        /// <param name="endOfNumberIndex"></param>
        /// <returns>Boolean value if a corresponding symbol is around or not.</returns>
        private static bool IsSymbolNearby(string previousLine, string currentLine, string nextLine, int startOfNumberIndex, int endOfNumberIndex)
        {
            var symbolFound = false;

            if (Constants.Symbols.Contains(previousLine[startOfNumberIndex - 1]) || Constants.Symbols.Contains(previousLine[startOfNumberIndex]) || Constants.Symbols.Contains(previousLine[startOfNumberIndex + 1])
                || Constants.Symbols.Contains(previousLine[endOfNumberIndex - 1]) || Constants.Symbols.Contains(previousLine[endOfNumberIndex]) || Constants.Symbols.Contains(previousLine[endOfNumberIndex + 1]))
            {
                symbolFound = true;
            }

            if (Constants.Symbols.Contains(currentLine[startOfNumberIndex - 1]) || Constants.Symbols.Contains(currentLine[endOfNumberIndex + 1]))
            {
                symbolFound = true;
            }

            if (Constants.Symbols.Contains(nextLine[startOfNumberIndex - 1]) || Constants.Symbols.Contains(nextLine[startOfNumberIndex]) || Constants.Symbols.Contains(nextLine[startOfNumberIndex + 1])
                || Constants.Symbols.Contains(nextLine[endOfNumberIndex + 1]) || Constants.Symbols.Contains(nextLine[endOfNumberIndex]) || Constants.Symbols.Contains(nextLine[endOfNumberIndex + 1]))
            {
                symbolFound = true;
            }

            return symbolFound;
        }

        /// <summary>
        /// Checks through the provided lines if they contain or not at most two numbers around the asterisk's position.
        /// If yes, returns the gear ratio (by default it's 0).
        /// </summary>
        /// <param name="previousLineNumbers"></param>
        /// <param name="currentLineNumbers"></param>
        /// <param name="nextLineNumbers"></param>
        /// <param name="indexOfAsterisk"></param>
        /// <returns>Integer value returning the gear ratio if conditions are matched.</returns>
        private static int GearRatio(List<Number> numbers, string previousLine, string currentLine, string nextLine, int indexOfAsterisk)
        {
            var gearRatio = 0;

            var previousLineNumbers = numbers
                .Where(n => n.Line == previousLine
                    && (n.FirstIndex == indexOfAsterisk - 1 || n.FirstIndex == indexOfAsterisk || n.FirstIndex == indexOfAsterisk + 1
                        || n.MiddleIndex == indexOfAsterisk - 1 || n.MiddleIndex == indexOfAsterisk || n.MiddleIndex == indexOfAsterisk + 1
                        || n.LastIndex == indexOfAsterisk - 1 || n.LastIndex == indexOfAsterisk || n.LastIndex == indexOfAsterisk + 1)).ToList();

            var currentLineNumbers = numbers
                .Where(n => n.Line == currentLine
                    && (n.LastIndex == indexOfAsterisk - 1 || n.FirstIndex == indexOfAsterisk + 1)).ToList();

            var nextLineNumbers = numbers
                .Where(n => n.Line == nextLine
                    && (n.FirstIndex == indexOfAsterisk - 1 || n.FirstIndex == indexOfAsterisk || n.FirstIndex == indexOfAsterisk + 1
                        || n.MiddleIndex == indexOfAsterisk - 1 || n.MiddleIndex == indexOfAsterisk || n.MiddleIndex == indexOfAsterisk + 1
                        || n.LastIndex == indexOfAsterisk - 1 || n.LastIndex == indexOfAsterisk || n.LastIndex == indexOfAsterisk + 1)).ToList();

            if (previousLineNumbers.Count + currentLineNumbers.Count + nextLineNumbers.Count == 2)
            {
                var gearNumbers = previousLineNumbers.Union(currentLineNumbers).Union(nextLineNumbers);
                gearRatio = gearNumbers.Aggregate(1, (acc, item) => acc * item.Value);
            }

            return gearRatio;
        }

        private class Number
        {
            public int Value { get; set; }
            public string Line { get; set; } = string.Empty;
            public int LineIndex { get; set; }
            public int FirstIndex { get; set; }
            public int MiddleIndex { get; set; }
            public int LastIndex { get; set; }
        }
    }
}