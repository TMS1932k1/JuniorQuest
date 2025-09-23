using UnityEngine;

public class Player_HurtState : PlayerState
{
    public Player_HurtState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerSFX.PlayHurt();
    }

    public override void Update()
    {
        base.Update();

        if (!player.isKnocked)
            stateMachine.ChangeState(player.idleState);

    }
}
