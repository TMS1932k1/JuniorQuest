using UnityEngine;

public abstract class Boss_Command : ICommand
{
    public Boss boss { get; private set; }
    public float executeTime { get; private set; }

    public Boss_Command(Boss boss, float executeTime)
    {
        this.boss = boss;
        this.executeTime = executeTime;
    }

    public abstract void Execute();

    public abstract void Undo();
}
