using Cent.Core.Entities;
using Cent.Core.Gamemodes.Abstractions;
using Cent.Core.Services;

namespace Cent.Core.Gamemodes;

public class SelectTunedInterval
{
    private readonly double minFrequency = 130.81;
    private readonly double maxFrequency = 1046.5;
    private readonly float playTimeInSeconds = 0.8f;
    private readonly int differenceInCentiles = 10;

    public SelectTunedInterval(int differenceInCentiles)
    {
        this.differenceInCentiles = differenceInCentiles;
    }

    public string Description => @"
Pick tuned interval!
1 - pick first
2 - pick second
Any buttom - repeat question";


    public SelectTunedIntervalQuestion GetQuestion()
    {
        var service = new PlaySoundService();

        var rand = new Random();

        var baseNote = rand.NextDouble() * (maxFrequency - minFrequency) + minFrequency;
        var isTunedFirst = rand.Next(0, 2) == 0;

        var firstSound = new Sound(baseNote);
        var secondSound = firstSound
            .GetNextUpperSoundByCents(100)
            .GetNextUpperSoundByCents(100)
            .GetNextUpperSoundByCents(100)
            .GetNextUpperSoundByCents(100)
            .GetNextUpperSoundByCents(100);

        var upperOrLower = rand.Next(0, 2) == 0;
        var thirdSound = upperOrLower ? secondSound.GetNextUpperSoundByCents(differenceInCentiles) :
            secondSound.GetNextLowerSoundByCents(differenceInCentiles);

        return new SelectTunedIntervalQuestion([firstSound,secondSound], [firstSound, thirdSound], isTunedFirst, service, playTimeInSeconds);
    }
}

public class SelectTunedIntervalQuestion : IQuestion
{
    private readonly IPlaySoundService playSoundService;
    private readonly float playTime;
    private readonly IEnumerable<Sound> tunedSounds;
    private readonly IEnumerable<Sound> untunedSounds;
    private readonly bool isTunedFirst;

    public SelectTunedIntervalQuestion(IEnumerable<Sound> lowerSound, IEnumerable<Sound> upperSound, bool isTunedFirst, IPlaySoundService playSoundService, float playTime)
    {
        this.tunedSounds = lowerSound;
        this.untunedSounds = upperSound;
        this.isTunedFirst = isTunedFirst;
        this.playSoundService = playSoundService;
        this.playTime = playTime;
    }

    public bool CheckAnswer(bool isTunedFirst) => this.isTunedFirst == isTunedFirst;

    public void PlayFirst()
    {
        if (isTunedFirst)
        {
            playSoundService.Play(tunedSounds, playTime);
            return;
        }

        playSoundService.Play(untunedSounds, playTime);
    }

    public void PlaySecond()
    {
        if (isTunedFirst)
        {
            playSoundService.Play(untunedSounds, playTime);
            return;
        }

        playSoundService.Play(tunedSounds, playTime);
    }
}
