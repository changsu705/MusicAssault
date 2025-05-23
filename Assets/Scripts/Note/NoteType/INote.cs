public interface INote
{
    float TargetTime { get; }
    int Line { get; }
    void Initialize(float time, float duration = 0f, int line = 0);
    void Tick();
}
