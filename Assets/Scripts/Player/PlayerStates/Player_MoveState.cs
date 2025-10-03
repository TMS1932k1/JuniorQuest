using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    private float stepsInterval = 0.4f;

    public Player_MoveState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerSFX.PlayMove(stepsInterval);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(Mathf.Sign(player.moveInput.x) * player.moveSpeed, rb.linearVelocityY);

        if (player.moveInput == Vector2.zero || (player.wallDetect && player.faceDir == player.moveInput.x))
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();

        playerSFX.StopMove();
    }
}
