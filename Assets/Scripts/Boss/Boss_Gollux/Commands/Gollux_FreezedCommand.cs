using UnityEngine;

public class Gollux_FreezedCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_FreezedCommand(Gollux gollux, string nameCommand, float executeTime) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();

        gollux.BeFreezed();
    }

    public override void Undo()
    {
        base.Undo();

        gollux.OutFreezed();
    }
}
