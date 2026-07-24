namespace Interfaces
{
    public interface IDifficultyProvider
    {
        float ElapsedGameTime { get; }
        float Progress { get; }
    }
}