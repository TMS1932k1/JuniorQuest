using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.wallDetect && !player.groundDetect)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (player.groundDetect)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
