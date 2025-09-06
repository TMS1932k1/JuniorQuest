using UnityEngine;
using UnityEngine.EventSystems;

public class Player_GroundedState : PlayerState
{
    private Player_Combat playerCombat;

    private float lastSlidePress;
    private float lastCounterPress;

    public Player_GroundedState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        playerCombat = player.GetComponent<Player_Combat>();
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.wallDetect && CanSlide())
        {
            stateMachine.ChangeState(player.slideState);
            lastSlidePress = Time.time;
        }

        // Change CounterState
        if (Input.GetKeyDown(KeyCode.Q) && CanCounter())
        {
            stateMachine.ChangeState(player.counterState);
            lastCounterPress = Time.time;
        }

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (skillsManager.HanldeInputUseSkill())
        {
            case ESkill_Type.FireBlade:
                {
                    if (skillsManager.fireBlade.CanBeUse())
                    {
                        skillsManager.fireBlade.PerformSkill();
                    }
                    break;
                }

            case ESkill_Type.Infeno:
                {
                    if (skillsManager.infeno.CanBeUse())
                        skillsManager.infeno.PerformSkill();

                    break;
                }

            case ESkill_Type.IcePrison:
                {
                    if (skillsManager.icePrison.CanBeUse())
                        skillsManager.icePrison.PerformSkill();

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

    private bool CanCounter()
    {
        return Time.time > lastCounterPress + playerCombat.counterCooldown;
    }
}
