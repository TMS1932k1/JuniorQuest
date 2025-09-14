using UnityEngine;

public class Gollux_RockDropCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_RockDropCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();

        gollux.SkillRockDrop();
    }
}
