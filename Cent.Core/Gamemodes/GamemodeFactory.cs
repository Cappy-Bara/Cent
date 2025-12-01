using Cent.Core.Entities;
using Cent.Core.Gamemodes.Abstractions;

namespace Cent.Core.Gamemodes;

public class GamemodeFactory
{
    public static IGamemode GetGamemode()
    {
        while (true)
        {
            Console.WriteLine("Select the gamemode:");
            Console.WriteLine($"1 - {SelectLowerSound.Name}");
            Console.WriteLine($"2 - {SelectTunedInterval.Name}");
            var gamemode = GetGamemodeNumber();

            switch (gamemode)
            {
                case 1:
                    var cents = GetCents();
                    return new SelectLowerSound(cents);
                case 2:
                    var centsInterval = GetCents();
                    var interval = GetInterval();
                    return new SelectTunedInterval(centsInterval, interval, false);
                default:
                    Console.WriteLine("Selected gamemode does not exist.");
                    return new SelectLowerSound(10);
            };
        }
    }

    private static Interval GetInterval() {         Console.WriteLine("Select the interval:");
        Console.WriteLine("1 - Minor second (1 semitone)");
        Console.WriteLine("2 - Major second (2 semitones)");
        Console.WriteLine("3 - Minor third (3 semitones)");
        Console.WriteLine("4 - Major third (4 semitones)");
        Console.WriteLine("5 - Perfect fourth (5 semitones)");
        Console.WriteLine("6 - Tritone (6 semitones)");
        Console.WriteLine("7 - Perfect fifth (7 semitones)");
        Console.WriteLine("8 - Minor sixth (8 semitones)");
        Console.WriteLine("9 - Major sixth (9 semitones)");
        Console.WriteLine("10 - Minor seventh (10 semitones)");
        Console.WriteLine("11 - Major seventh (11 semitones)");
        Console.WriteLine("12 - Octave (12 semitones)");
        while (true)
        {
            var response = Console.ReadLine();
            var success = int.TryParse(response, out int intervalNumber);
            if (!success || intervalNumber < 1 || intervalNumber > 12)
            {
                Console.WriteLine("Enter a valid number.");
                continue;
            }
            
            return new Interval(intervalNumber);
        }
    }

    private static int GetGamemodeNumber()
    {
        var success = false;
        int gamemode = 0;

        while (!success)
        {
            var response = Console.ReadLine();
            success = int.TryParse(response, out gamemode);
            if (!success)
                Console.WriteLine("Enter a valid number.");

            return gamemode;
        }

        return gamemode;
    }

    private static int GetCents()
    {
        Console.WriteLine("Select the difficulity (in cents):");
        Console.WriteLine("(semitone - 100):");

        var success = false;
        int differenceInCentiles = 0;

        while (!success)
        {
            var response = Console.ReadLine();
            success = int.TryParse(response, out differenceInCentiles);
            if (success)
                return differenceInCentiles;

                Console.WriteLine("Enter a valid number.");
        }

        return differenceInCentiles;
    }
}
