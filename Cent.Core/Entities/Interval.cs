namespace Cent.Core.Entities;

public record Interval(int Semitones)
{
    public static Interval Unisono => new(0);
    public static Interval MinorSecond => new(1);
    public static Interval MajorSecond => new(2);
    public static Interval MinorThird => new(3);
    public static Interval MajorThird => new(4);
    public static Interval Fourth => new(5);
    public static Interval Tritone => new(6);
    public static Interval Fifth => new(7);
    public static Interval MinorSixth => new(8);
    public static Interval MajorSixth => new(9);
    public static Interval MinorSeventh => new(10);
    public static Interval MajorSeventh => new(11);
    public static Interval Octave => new(12);
}
