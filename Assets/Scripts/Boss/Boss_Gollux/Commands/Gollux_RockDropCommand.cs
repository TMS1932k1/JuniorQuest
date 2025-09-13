using UnityEngine;

public class Gollux_RockDropCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_RockDropCommand(Gollux gollux, float executeTime = 0f) : base(gollux, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        gollux.SkillRockDrop();
    }

    public override void Undo()
    {
        gollux.Idle();
    }
}
