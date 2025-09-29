using UnityEngine;

public class PlayerState : EntityState
{
    public Player player;
    protected Player_SFX playerSFX;
    protected Player_SkillsManager playerSkillsManager;
    protected InputSystemSet input;


    public PlayerState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        this.player = player;
        playerSkillsManager = player.GetComponent<Player_SkillsManager>();
        playerSFX = player.GetComponent<Player_SFX>();

        rb = player.rb;
        anim = player.anim;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (playerSkillsManager.HanldeInputUseSkill())
        {
            case ESkill_Type.ShieldBarrier:
                {
                    if (playerSkillsManager.shieldBarrier.CanBeUse())
                        playerSkillsManager.shieldBarrier.PerformSkill();

                    break;
                }

            case ESkill_Type.Comeback:
                {
                    if (playerSkillsManager.comeback.CanBeUse())
                        playerSkillsManager.comeback.PerformSkill();

                    break;
                }

            case ESkill_Type.Invisibility:
                {
                    if (playerSkillsManager.invisibility.CanBeUse())
                        playerSkillsManager.invisibility.PerformSkill();

                    break;
                }

            case ESkill_Type.BattleCry:
                {
                    if (playerSkillsManager.battleCry.CanBeUse())
                        playerSkillsManager.battleCry.PerformSkill();

                    break;
                }

            default:
                break;
        }
    }
}
