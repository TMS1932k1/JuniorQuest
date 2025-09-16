using UnityEngine;

public class Gollux_DeathCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_DeathCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();

        gollux.Death();
    }
}
