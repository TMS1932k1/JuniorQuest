using UnityEngine;

public class Player_DeathState : PlayerState
{
    public Player_DeathState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StopMoving();

        player.isDead = true;
        rb.simulated = false;
    }
}
