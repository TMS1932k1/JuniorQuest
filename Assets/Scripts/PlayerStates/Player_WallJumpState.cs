using UnityEngine;

public class Player_WallJumpState : PlayerState
{
    public Player_WallJumpState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpVelocity.x * -player.faceDir, player.wallJumpVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }

        CancleIfNeed();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void CancleIfNeed()
    {
        if (player.wallDetect && !player.groundDetect) // When wall detection
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
