using Cent.Core.Entities;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Cent.Core.Services;

public interface IPlaySoundService
{
    public void Play(Sound sound, float playTime);
    public void Play(IEnumerable<Sound> sounds, float playTime);
}

public class PlaySoundService : IPlaySoundService
{   
    public void Play(Sound sound, float playTime)
    {
        Play([sound], playTime);
    }

    public void Play(IEnumerable<Sound> sounds, float playTime)
    {
        var signals = sounds.Select(sound => new SignalGenerator()
        {
            Gain = 0.2,
            Frequency = sound.Frequency,
            Type = SignalGeneratorType.Sin
        }.Take(TimeSpan.FromSeconds(playTime)));

        var mix = new MixingSampleProvider(signals);

        using var wo = new WaveOutEvent();
        wo.Init(mix);
        wo.Play();
        while (wo.PlaybackState == PlaybackState.Playing)
        {
            Thread.Sleep(500);
        }
    }
}
