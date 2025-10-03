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
        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.jumpState);

        // Change FallState
        if (rb.linearVelocityY < 0 && !player.groundDetect)
            stateMachine.ChangeState(player.fallState);

        // Change AttackState
        if (input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.basicAttackState);

        // Change SlideState
        if (input.Player.Dash.WasPressedThisFrame() && !player.wallDetect && CanSlide())
        {
            stateMachine.ChangeState(player.slideState);
            lastSlidePress = Time.time;
        }

        // Change CounterState
        if (input.Player.Counter.WasPressedThisFrame() && CanCounter())
        {
            stateMachine.ChangeState(player.counterState);
            lastCounterPress = Time.time;
        }

        HandleUseSkills();
    }

    private void HandleUseSkills()
    {
        switch (playerSkillsManager.HanldeInputUseSkill())
        {
            case ESkill_Type.FireBlade:
                {
                    if (playerSkillsManager.fireBlade.CanBeUse())
                        playerSkillsManager.fireBlade.PerformSkill();

                    break;
                }

            case ESkill_Type.Infeno:
                {
                    if (playerSkillsManager.infeno.CanBeUse())
                        playerSkillsManager.infeno.PerformSkill();

                    break;
                }

            case ESkill_Type.IcePrison:
                {
                    if (playerSkillsManager.icePrison.CanBeUse())
                        playerSkillsManager.icePrison.PerformSkill();

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
