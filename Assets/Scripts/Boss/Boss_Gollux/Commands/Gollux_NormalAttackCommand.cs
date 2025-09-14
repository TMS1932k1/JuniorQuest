using UnityEngine;

public class Gollux_NormalAttackCommand : Boss_Command
{
    private Gollux gollux;

    public Gollux_NormalAttackCommand(Gollux gollux, string nameCommand, float executeTime = 0f) : base(gollux, nameCommand, executeTime)
    {
        this.gollux = gollux;
    }
}
