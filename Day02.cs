using System.Security.Cryptography;

public static class Day02
{
    enum Option
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    enum Outcome
    {
        Lost = 0,
        Draw = 3,
        Won = 6
    }

    record Game(Option Elf, Option Me)
    {
        public int Score => (int)Me + (int)Outcome;

        public Outcome Outcome =>
            (Elf, Me) switch
            {
                (Option elf, Option me) when (Elf == Me) => Outcome.Draw,
                (Option.Rock, Option.Scissors) => Outcome.Lost,
                (Option.Scissors, Option.Paper) => Outcome.Lost,
                (Option.Paper, Option.Rock) => Outcome.Lost,
                _ => Outcome.Won
            };
    }

    static Option ParseElf(char c) =>
        c switch
        {
            'A' => Option.Rock,
            'B' => Option.Paper,
            _ => Option.Scissors,
        };

    static Option ParseMe(char c) =>
        c switch
        {
            'X' => Option.Rock,
            'Y' => Option.Paper,
            _ => Option.Scissors,
        };

    static Outcome ParseOutcome(char c) =>
        c switch
        {
            'X' => Outcome.Lost,
            'Y' => Outcome.Draw,
            _ => Outcome.Won,
        };
    static Game ParseGame(string line) =>
        new Game(
            ParseElf(line[0]),
            ParseMe(line[2]));

    static Game ParseGameByOutcome(string line)
    {
        var elf = ParseElf(line[0]);
        var outcome = ParseOutcome(line[2]);

        var me = (elf, outcome) switch
        {
            (Option.Rock, Outcome.Won) => Option.Paper,
            (Option.Scissors, Outcome.Won) => Option.Rock,
            (Option.Paper, Outcome.Won) => Option.Scissors,
            (Option.Rock, Outcome.Lost) => Option.Scissors,
            (Option.Scissors, Outcome.Lost) => Option.Paper,
            (Option.Paper, Outcome.Lost) => Option.Rock,
            _ => elf,
        };

        return new Game(elf, me);
    }

    public static void Solve()
    {
        var input = File.ReadAllLines("input/02.txt");

        var games = input.Select(ParseGame);
        Console.WriteLine(games.Sum(g => g.Score));

        var gamesByOutcome = input.Select(ParseGameByOutcome);
        Console.WriteLine(gamesByOutcome.Sum(g => g.Score));
    }
}