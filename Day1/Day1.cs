namespace advent_of_code_2023.Day1
{
    public class Day1
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
            Console.WriteLine("---");
        }
    }
}