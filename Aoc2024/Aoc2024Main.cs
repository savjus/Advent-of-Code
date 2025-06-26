using System.Diagnostics;
using Aoc2024;

// taken from https://github.com/rtrinh3/AdventOfCode/blob/master/README.md
// run day with .\Aoc2023.exe 01 "day01-input.txt" within  bin/debug/net9.0 
// add inputs and execute the code in the filepath:
//C:\Users\salvp\source\repos\Advent Of Code\AoC_2023\Aoc2023\Aoc2023\bin\Debug\net8.0

//list of days that I don't understand fully
//Day 09  math
//
namespace Aoc2024
{
    internal class Aoc2024Main
    {
        static void Main(string[] args)
        {
            string? day = args.ElementAtOrDefault(0);
            string? input = args.ElementAtOrDefault(1);

            if (File.Exists(input))
            {
                input = File.ReadAllText(input);
            }
            if (!int.TryParse(day, out int dayValue) || dayValue < 1 || dayValue > 25)
            {
                throw new Exception($"Bad day: {day}");
            }
            
            string dayClassName = "Aoc2024.Day" + dayValue.ToString("00");
            Console.WriteLine($"Loading: {dayClassName}");
            Stopwatch initTimer = Stopwatch.StartNew();
            var dayClass = typeof(Aoc2024Main).Assembly.GetType(dayClassName);
            if (dayClass == null)
                throw new Exception("dayclass null");
            var dayConstructor = dayClass.GetConstructor(new[] {typeof(string) });
            IAocDay dayInstance = (IAocDay)dayConstructor.Invoke(new object[] { input });
            Console.WriteLine($"Time: {initTimer.Elapsed}");

            Console.WriteLine("\nPart 1");
            Stopwatch partOneTimer = Stopwatch.StartNew();
            Int64 partOneAnswer = dayInstance.Part1();
            Console.WriteLine($"Time: {partOneTimer.Elapsed}");
            Console.WriteLine(partOneAnswer);

            Console.WriteLine("\nPart 2");
            Stopwatch partTwoTimer = Stopwatch.StartNew();
            Int64 partTwoAnswer = dayInstance.Part2();
            Console.WriteLine($"Time: {partTwoTimer.Elapsed}");
            Console.WriteLine(partTwoAnswer);
        }
    }
}