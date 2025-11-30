namespace Cent.Core.Gamemodes.Abstractions;

public interface IGamemode
{
    public IQuestion GetQuestion();
    public string Description { get; }
}
