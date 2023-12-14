using System.Text;

namespace advent_of_code_2023.Day1
{
    public class Day01
    {
        public static void SolvePart1()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day1/input.txt");

            foreach (var line in input)
            {
                var charArray = line.ToCharArray();

                var firstNumber = 0;
                var secondNumber = 0;
                var index = 0;
                var numberFound = false;

                while (index < charArray.Length && !numberFound)
                {
                    numberFound = int.TryParse(charArray[index].ToString(), out firstNumber);

                    index++;
                }

                index = charArray.Length - 1;
                numberFound = false;

                while (index >= 0 && !numberFound)
                {
                    numberFound = int.TryParse(charArray[index].ToString(), out secondNumber);

                    index--;
                }

                output += int.Parse($"{firstNumber}{secondNumber}");
            }

            Console.WriteLine($"Result of Day 1 - Part 1 is {output}");
        }

        public static void SolvePart2()
        {
            var output = 0;
            var input = File.ReadAllLines("../../../Day1/input.txt");

            foreach (var line in input)
            {
                var charArray = line.ToCharArray();

                var firstNumber = 0;
                var secondNumber = 0;
                var stringBuilder = new StringBuilder();

                foreach (var character in charArray)
                {
                    if (char.IsNumber(character))
                    {
                        firstNumber = firstNumber == 0 ? int.Parse(character.ToString()) : firstNumber;
                        secondNumber = int.Parse(character.ToString());

                        if (secondNumber != 0)
                        {
                            stringBuilder.Clear();
                        }
                    }
                    else
                    {
                        stringBuilder.Append(character);
                        var numberFound = FindNumberInString(stringBuilder.ToString());

                        firstNumber = firstNumber == 0 ? numberFound : firstNumber;
                        secondNumber = numberFound != 0 ? numberFound : secondNumber;
                    }
                }

                output += int.Parse($"{firstNumber}{secondNumber}");
            }

            Console.WriteLine($"Result of Day 1 - Part 2 is {output}");
            Console.WriteLine("---");
        }

        private static int FindNumberInString(string stringNumber)
        {
            while (stringNumber.Length > 0)
            {
                var result = ConvertStringToNumber(stringNumber);

                if (result != 0)
                {
                    return result;
                }

                stringNumber = stringNumber.Substring(1);
            }

            return 0;
        }

        private static int ConvertStringToNumber(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber))
            {
                return 0;
            }

            if (Constants.StringNumberPairs.TryGetValue(stringNumber, out int number))
            {
                return number;
            }

            var splits = stringNumber.Split('-');

            if (splits.Length == 2)
            {
                int firstSplit = 0;
                int secondSplit = 0;

                if (Constants.StringNumberPairs.TryGetValue(splits[0], out firstSplit)
                    && Constants.StringNumberPairs.TryGetValue(splits[1], out secondSplit))
                {
                    return firstSplit + secondSplit;
                }
            }

            return 0;
        }
    }
}