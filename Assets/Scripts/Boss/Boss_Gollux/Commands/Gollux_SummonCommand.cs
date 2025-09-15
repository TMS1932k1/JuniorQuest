using UnityEngine;

public class Gollux_SummonCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_SummonCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();

        gollux.SkillSummon();
    }
}
