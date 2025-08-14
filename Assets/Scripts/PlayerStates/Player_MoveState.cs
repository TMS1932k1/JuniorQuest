using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(Input.GetAxisRaw("Horizontal") * player.moveSpeed, rb.linearVelocityY);

        // Change IdleState
        if ((!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) ||
            (player.wallDetect && player.faceDir == Input.GetAxisRaw("Horizontal")))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
