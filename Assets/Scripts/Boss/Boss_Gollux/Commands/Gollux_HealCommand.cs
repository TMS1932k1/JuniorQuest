using UnityEngine;

public class Gollux_HealCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_HealCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        base.Execute();
        gollux.SkillSummonDetroy();
    }

    public override void Undo()
    {
        base.Undo();
        gollux.SkillSummonHeal();
    }
}
