using UnityEngine;

public class Gollux_Attack1Command : ICommand
{
    private Gollux gollux;

    public Gollux_Attack1Command(Gollux gollux)
    {
        this.gollux = gollux;
    }

    public void Execute()
    {
        gollux.SkillAttack1();
    }

    public void Undo()
    {
        gollux.Idle();
    }
}
