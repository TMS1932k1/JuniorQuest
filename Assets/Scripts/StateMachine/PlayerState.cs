using UnityEngine;

public class PlayerState : EntityState
{
    public Player player;
    private Player_SkillsManager skillsManager;

    private float lastDashPress;

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

        // Change DashState
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.wallDetect && !player.isDead && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
            lastDashPress = Time.time;
        }

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (skillsManager.HanldeInputUseSkill())
        {
            case SkillType.ShieldBarrier:
                {
                    if (skillsManager.shieldBarrier.CanBeUse())
                    {
                        skillsManager.shieldBarrier.SetLastTimeUsed();
                        Debug.Log("Use " + skillsManager.shieldBarrier.skillData.skillName);
                    }
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
                    {
                        skillsManager.battleCry.SetLastTimeUsed();
                        Debug.Log("Use " + skillsManager.battleCry.skillData.skillName);
                    }
                    break;
                }

            default:
                break;
        }
    }

    private bool CanDash()
    {
        return Time.time > lastDashPress + player.dashCooldown
            && stateMachine.currentState != player.counterState;
    }
}
