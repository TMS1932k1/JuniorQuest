using UnityEngine;

public class Player_DeathState : PlayerState
{
    private Player_VFX playerVFX;

    public Player_DeathState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        playerVFX = player.GetComponent<Player_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        StopMoving();

        playerVFX.ResetVFX();

        player.isDead = true;
        rb.simulated = false;
    }
}
