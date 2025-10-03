using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.transform.localScale = new Vector3(-1, 1, 1);
    }

    public override void Update()
    {
        base.Update();

        HandleSlideSpeed();
        CancleIfNeed();

        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);
    }

    public override void Exit()
    {
        base.Exit();

        player.transform.localScale = new Vector3(1, 1, 1);

        // Flip on ground when face direct diffrent with movement direct
        if (GetValueInput(player.moveInput.x) != player.faceDir && player.groundDetect)
            player.Flip();
    }

    private int GetValueInput(float inputValue)
    {
        if (inputValue > 0) return 1;
        if (inputValue < 0) return -1;
        return 0;
    }

    private void HandleSlideSpeed()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(0, rb.linearVelocityY);
        else
            player.SetVelocity(0, rb.linearVelocityY * player.wallSlideMultiplier);
    }

    private void CancleIfNeed()
    {
        if (!player.wallDetect && !player.groundDetect) // When on air and not wall detection
        {
            stateMachine.ChangeState(player.fallState);
        }
        else if (player.groundDetect) // When ground detection
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
