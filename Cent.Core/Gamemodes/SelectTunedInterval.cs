using Cent.Core.Entities;
using Cent.Core.Gamemodes.Abstractions;
using Cent.Core.Services;

namespace Cent.Core.Gamemodes;

public class SelectTunedInterval : IGamemode
{
    private readonly double minFrequency = 130.81;
    private readonly double maxFrequency = 1046.5;
    private readonly float playTimeInSeconds = 0.8f;
    private readonly int differenceInCentiles = 10;
    private readonly Interval interval;
    private bool withHarmonic = false;

    public SelectTunedInterval(int differenceInCentiles, Interval interval, bool withHarmonic)
    {
        this.differenceInCentiles = differenceInCentiles;
        this.interval = interval;
        this.withHarmonic = withHarmonic;
    }

    public static string Name => "Select valid interval";

    public string Description => @"
Pick tuned interval!
1 - pick first
2 - pick second
Any buttom - repeat question";


    public IQuestion GetQuestion()
    {
        var service = new PlaySoundService();

        var rand = new Random();

        var baseNote = rand.NextDouble() * (maxFrequency - minFrequency) + minFrequency;
        var isTunedFirst = rand.Next(0, 2) == 0;

        var firstSound = new Sound(baseNote);
        var secondSound = GetBaseSecondSound(firstSound);

        var upperOrLower = rand.Next(0, 2) == 0;
        var thirdSound = upperOrLower ? secondSound.GetNextUpperSoundByCents(differenceInCentiles) :
            secondSound.GetNextLowerSoundByCents(differenceInCentiles);

        return new SelectTunedIntervalQuestion([firstSound,secondSound], [firstSound, thirdSound],
            isTunedFirst, service, playTimeInSeconds, withHarmonic);
    }

    private Sound GetBaseSecondSound(Sound firstSound) 
    {
        var semitones = interval.Semitones;

        var secondSound = firstSound;

        for (int i = 0; i < semitones; i++)
        {
            secondSound = secondSound.GetNextUpperSoundByCents(100);
        }

        return secondSound;
    }
}

public class SelectTunedIntervalQuestion : IQuestion
{
    private readonly IPlaySoundService playSoundService;
    private readonly float playTime;
    private readonly bool withHarmonic;
    private readonly IEnumerable<Sound> tunedSounds;
    private readonly IEnumerable<Sound> untunedSounds;
    private readonly bool isTunedFirst;

    public SelectTunedIntervalQuestion(IEnumerable<Sound> lowerSound, IEnumerable<Sound> upperSound,
        bool isTunedFirst, IPlaySoundService playSoundService, float playTime, bool withHarmonic)
    {
        this.tunedSounds = lowerSound;
        this.untunedSounds = upperSound;
        this.isTunedFirst = isTunedFirst;
        this.playSoundService = playSoundService;
        this.playTime = playTime;
        this.withHarmonic = withHarmonic;
    }

    public bool CheckAnswer(bool isTunedFirst) => this.isTunedFirst == isTunedFirst;

    public void PlayFirst()
    {
        if (isTunedFirst)
        {
            playSoundService.Play(tunedSounds.First(), playTime);
            playSoundService.Play(tunedSounds.Skip(1).First(), playTime);

            if(withHarmonic)
                playSoundService.Play(tunedSounds, playTime);
            return;
        }

        playSoundService.Play(untunedSounds.First(), playTime);
        playSoundService.Play(untunedSounds.Skip(1).First(), playTime);
        if (withHarmonic)
            playSoundService.Play(untunedSounds, playTime);
    }

    public void PlaySecond()
    {
        if (isTunedFirst)
        {
            playSoundService.Play(untunedSounds.First(), playTime);
            playSoundService.Play(untunedSounds.Skip(1).First(), playTime);
            
            if (withHarmonic)
                playSoundService.Play(untunedSounds, playTime);
            
            return;
        }

        playSoundService.Play(tunedSounds.First(), playTime);
        playSoundService.Play(tunedSounds.Skip(1).First(), playTime);

        if (withHarmonic)
            playSoundService.Play(tunedSounds, playTime);
    }
}
