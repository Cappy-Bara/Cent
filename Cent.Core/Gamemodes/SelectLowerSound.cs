using Cent.Core.Entities;
using Cent.Core.Gamemodes.Abstractions;
using Cent.Core.Services;

namespace Cent.Core.Gamemodes;

public class SelectLowerSound : IGamemode
{
    private readonly double minFrequency = 130.81;
    private readonly double maxFrequency = 1046.5;
    private readonly float playTimeInSeconds = 0.5f;
    private readonly int differenceInCentiles = 10;

    public SelectLowerSound(int differenceInCentiles)
    {
        this.differenceInCentiles = differenceInCentiles;
    }

    public static string Name => "Select lower sound";

    public string Description => @"
Pick the lower sound!
1 - pick first sound
2 - pick second sound
Any buttom - repeat question";

    public IQuestion GetQuestion()
    {
        var service = new PlaySoundService();

        var rand = new Random();

        var baseNote = rand.NextDouble() * (maxFrequency - minFrequency) + minFrequency; 
        var isLowerFirst = rand.Next(0, 2) == 0;

        var firstSound = new Sound(baseNote);
        var secondSound = firstSound.GetNextUpperSoundByCents(differenceInCentiles);

        return new SelectHigherSoundQuestion(firstSound, secondSound, isLowerFirst, service, playTimeInSeconds);
    }
}

public class SelectHigherSoundQuestion : IQuestion
{
    private readonly IPlaySoundService playSoundService;
    private readonly float playTime;
    private readonly Sound lowerSound;
    private readonly Sound higherSound;
    private readonly bool isLowerFirst;

    public SelectHigherSoundQuestion(Sound lowerSound, Sound upperSound, bool isLowerFirst, IPlaySoundService playSoundService, float playTime)
    {
        this.lowerSound = lowerSound;
        this.higherSound = upperSound;
        this.isLowerFirst = isLowerFirst;
        this.playSoundService = playSoundService;
        this.playTime = playTime;
    }

    public bool CheckAnswer(bool isLowerFirst) => this.isLowerFirst == isLowerFirst;

    public void PlayFirst()
    {
        if (isLowerFirst)
        {
            playSoundService.Play(lowerSound, playTime);
            return;
        }
            
        playSoundService.Play(higherSound, playTime);
    }

    public void PlaySecond()
    {
        if (isLowerFirst)
        {
            playSoundService.Play(higherSound, playTime);
            return;
        }

        playSoundService.Play(lowerSound, playTime);
    }
}
