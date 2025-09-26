using UnityEngine;

public class Boss_Command : ICommand
{
    public Boss boss { get; private set; }
    public float executeTime { get; private set; }
    private string nameCommand;


    private Animator anim;
    protected Entity_SFX entitySFX;


    public Boss_Command(Boss boss, string nameCommand, float executeTime)
    {
        this.boss = boss;
        this.executeTime = executeTime;
        this.nameCommand = nameCommand;

        anim = boss.GetComponentInChildren<Animator>();
        entitySFX = boss.GetComponent<Entity_SFX>();
    }

    public virtual void Execute()
    {
        anim.SetBool(nameCommand, true);
    }

    public virtual void Undo()
    {
        anim.SetBool(nameCommand, false);
    }
}
