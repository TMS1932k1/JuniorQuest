using UnityEngine;

public class Player_SlideState : EntityState
{
    public Player_SlideState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.slideDuration;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.slideSpeed * player.faceDir, 0);

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
    }

    private bool CancleIfNeed()
    {
        if (player.wallDetect) return true;
        return false;
    }
}
