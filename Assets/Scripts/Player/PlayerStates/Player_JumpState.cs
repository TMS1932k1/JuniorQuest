using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {

    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
