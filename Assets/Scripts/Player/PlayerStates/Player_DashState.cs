using UnityEngine;

public class Player_DashState : Player_AiredState
{
    private float originGravity;

    public Player_DashState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;

        playerSFX.PlayDash();
        originGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.faceDir, 0);

        if (stateTimer < 0 || CancleIfNeed())
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        StopMoving();
        rb.gravityScale = originGravity;

        lastDashPress = Time.time;
    }

    private bool CancleIfNeed()
    {
        if (player.wallDetect) return true;
        return false;
    }
}
