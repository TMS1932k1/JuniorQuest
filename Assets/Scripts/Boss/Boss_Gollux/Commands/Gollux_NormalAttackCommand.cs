using UnityEngine;

public class Gollux_NormalAttackCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_NormalAttackCommand(Gollux gollux, float executeTime = 0f) : base(gollux, executeTime)
    {
        this.gollux = gollux;
    }

    public override void Execute()
    {
        gollux.SkillNormalAttack();
    }

    public override void Undo()
    {
        gollux.Idle();
    }
}
