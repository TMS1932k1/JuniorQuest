using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StopMoving();
    }

    public override void Update()
    {
        base.Update();


        // Not allow move when detect wall before face
        if (player.wallDetect && player.faceDir == Input.GetAxisRaw("Horizontal"))
            return;

        // Change MoveState
        if (player.moveInput.x != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
