public enum TaskPriority
{
    Low,
    Medium,
    High
}

public interface ITask
{
    string Name { get; }

    bool IsDone { get; set; }

    TaskPriority Priority { get; }

    void ExecuteStart();

    void ExecuteUpdate();

}
