using UnityEngine;

public class PlayerState : EntityState
{
    public Player player;
    protected Player_SkillsManager skillsManager;


    public PlayerState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        this.player = player;
        skillsManager = player.GetComponent<Player_SkillsManager>();

        rb = player.rb;
        anim = player.anim;
    }

    public override void Update()
    {
        base.Update();

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (skillsManager.HanldeInputUseSkill())
        {
            case SkillType.ShieldBarrier:
                {
                    if (skillsManager.shieldBarrier.CanBeUse())
                        skillsManager.shieldBarrier.PerformSkill();

                    break;
                }

            case SkillType.Comeback:
                {
                    if (skillsManager.comeback.CanBeUse())
                        skillsManager.comeback.PerformSkill();

                    break;
                }

            case SkillType.Invisibility:
                {
                    if (skillsManager.invisibility.CanBeUse())
                        skillsManager.invisibility.PerformSkill();

                    break;
                }

            case SkillType.BattleCry:
                {
                    if (skillsManager.battleCry.CanBeUse())
                        skillsManager.battleCry.PerformSkill();

                    break;
                }

            default:
                break;
        }
    }
}
