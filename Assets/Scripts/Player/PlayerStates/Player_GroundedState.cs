using UnityEngine;
using UnityEngine.EventSystems;

public class Player_GroundedState : PlayerState
{
    private Player_SkillsManager skillsManager;
    private float lastSlidePress;

    public Player_GroundedState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        skillsManager = player.GetComponent<Player_SkillsManager>();
    }

    public override void Update()
    {
        base.Update();

        // Change JumpState
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
        }

        // Change FallState
        if (rb.linearVelocityY < 0 && !player.groundDetect)
        {
            stateMachine.ChangeState(player.fallState);
        }

        // Change AttackState
        if (Input.GetKeyDown(KeyCode.Mouse0)
            && !(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())) // Don't click UI
        {
            stateMachine.ChangeState(player.basicAttackState);
        }

        // Change SlideState
        if (Input.GetKeyDown(KeyCode.Z) && !player.wallDetect && CanSlide())
        {
            stateMachine.ChangeState(player.slideState);
            lastSlidePress = Time.time;
        }

        // Change CounterState
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterState);
        }

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (skillsManager.HanldeInputUseSkill())
        {
            case SkillType.WindBlade:
                {
                    if (skillsManager.windBlade.CanBeUse())
                    {
                        skillsManager.windBlade.SetLastTimeUsed();
                        Debug.Log("Use " + skillsManager.windBlade.skillData.skillName);
                    }
                    break;
                }

            case SkillType.Infeno:
                {
                    if (skillsManager.infeno.CanBeUse())
                    {
                        skillsManager.infeno.SetLastTimeUsed();
                        Debug.Log("Use " + skillsManager.infeno.skillData.skillName);
                    }
                    break;
                }

            case SkillType.IcePrison:
                {
                    if (skillsManager.icePrison.CanBeUse())
                    {
                        skillsManager.icePrison.SetLastTimeUsed();
                        Debug.Log("Use " + skillsManager.icePrison.skillData.skillName);
                    }
                    break;
                }

            default:
                break;
        }
    }

    private bool CanSlide()
    {
        return Time.time > lastSlidePress + player.slideCooldown;
    }
}
