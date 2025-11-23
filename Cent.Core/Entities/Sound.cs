namespace Cent.Core.Entities;

public class Sound(double freq)
{
    double val = 1f / 12f;
    public double Frequency { get; } = freq;

    public Sound GetNextUpperSoundByCentiles(int centiles)
    {
        var nextTone = GetNextSoundFrequency();
        var freqDiff = nextTone - Frequency;
        var centileDiff = freqDiff / 100;
        var newFreq = Frequency + centileDiff * centiles;
        return new Sound(newFreq);
    }

    public Sound GetNextLowerSoundByCentiles(int centiles)
    {
        var nextTone = GetPreviousSoundFrequency();
        var freqDiff = Frequency - nextTone;
        var centileDiff = freqDiff / 100;
        var newFreq = Frequency - centileDiff * centiles;
        return new Sound(newFreq);
    }

    public double GetNextSoundFrequency()
    {
        return Frequency * Math.Pow(2 , val);
    }

    public double GetPreviousSoundFrequency()
    {
        return Frequency / Math.Pow(2, val);
    }
}
