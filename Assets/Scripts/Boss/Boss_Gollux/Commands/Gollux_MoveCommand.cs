using UnityEngine;

public class Gollux_MoveCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_MoveCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();

        gollux.Move();
    }

    public override void Undo()
    {
        base.Undo();

        gollux.StopMove();
    }
}
