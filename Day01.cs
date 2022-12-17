public static class Day01
{
    public static void Solve()
    {
        var input = File.ReadAllText("input/01.txt");
        var textByElf = input.Split(Environment.NewLine + Environment.NewLine);
        var linesByElf = textByElf.Select(text => text.Split(Environment.NewLine));
        var caloriesByElf = linesByElf.Select(elf => elf.Sum(line => int.Parse(line)));

        Console.WriteLine(caloriesByElf.Max());
        Console.WriteLine(caloriesByElf.OrderDescending().Take(3).Sum());
    }
}