using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.basicAttackState);
        }

        // Change SlideState
        if (Input.GetKeyDown(KeyCode.Z) && !player.wallDetect)
        {
            stateMachine.ChangeState(player.slideState);
        }
    }
}
