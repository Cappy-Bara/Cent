namespace Cent.Core.Gamemodes.Abstractions;

public interface IQuestion
{
    public bool CheckAnswer(bool isFirstValid);
    public void PlayFirst();
    public void PlaySecond();
}
