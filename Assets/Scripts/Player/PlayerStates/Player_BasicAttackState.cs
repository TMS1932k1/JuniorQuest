using System.Linq;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private int currentAttackIndex;
    private int attackIndex;
    private float resetTime = 1f;
    private float lastAttacktime;
    private bool haveAttackQueue;
    private bool isAttackEnd;
    private bool isAllAnimAttack;


    public Player_BasicAttackState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        attackIndex = player.attackVelocities.Count();
    }

    public override void Enter()
    {
        base.Enter();

        StopMoving();
        ResetCombo();

        isTrigger = false;
        haveAttackQueue = false;
        isAttackEnd = false;
        isAllAnimAttack = false;
        currentAttackIndex++;
    }

    public override void Update()
    {
        base.Update();

        // OnClick left mouse to queue attack
        // Not allowed when it is end of attack and was full combo attack
        if (input.Player.Attack.WasPressedThisFrame() && !isAttackEnd && currentAttackIndex < attackIndex)
        {
            haveAttackQueue = true;
        }

        if (isAttackEnd && !isAllAnimAttack)
        {
            isAllAnimAttack = true;
            anim.SetTrigger(PlayerAnimationStrings.ATTACK_END_TRIGGER);
        }

        if (isTrigger)
        {
            StopMoving();

            if (haveAttackQueue)
            {
                anim.SetBool(nameState, false);
                player.EnterAttackQueue(player.basicAttackState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
        else
        {
            anim.SetInteger(PlayerAnimationStrings.ATTACK_INDEX_PARAM, currentAttackIndex);

            //GenerateAttackVelocity(); FIXING BUG
        }
    }

    public override void Exit()
    {
        base.Exit();

        lastAttacktime = Time.time;
    }

    public override void CallTrigger()
    {
        if (haveAttackQueue || isAttackEnd)
        {
            isTrigger = true;
        }
        else if (!isAttackEnd)
        {
            isAttackEnd = true;
        }
    }

    /// <summary>
    /// Reset (attackIndex) when overtime (resetTime) or fulled combo attack
    /// </summary>
    private void ResetCombo()
    {
        if (Time.time > lastAttacktime + resetTime)
        {
            currentAttackIndex = 0;
        }

        if (currentAttackIndex >= attackIndex)
        {
            currentAttackIndex = 0;
        }
    }

    private void GenerateAttackVelocity()
    {
        player.SetVelocity(
            player.attackVelocities[currentAttackIndex - 1].x * player.faceDir,
            player.attackVelocities[currentAttackIndex - 1].y
        );
    }
}
