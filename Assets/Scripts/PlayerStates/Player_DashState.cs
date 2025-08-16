using UnityEngine;

public class Player_DashState : EntityState
{
    private float originGravity;

    public Player_DashState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;

        originGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.faceDir, 0);

        if (stateTimer < 0 || CancleIfNeed())
        {
            if (player.groundDetect)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopMoving();
        rb.gravityScale = originGravity;
    }

    private bool CancleIfNeed()
    {
        if (player.wallDetect) return true;
        return false;
    }
}
