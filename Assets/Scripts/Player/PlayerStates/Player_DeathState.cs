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
        isTrigger = false;

        StopMoving();

        playerVFX.ResetVFX();
        playerSFX.PlayDeath();

        player.isDead = true;
        rb.simulated = false;
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
            SaveManager.instance.LoadGame();
    }

    public override void Exit()
    {
        base.Exit();

        player.isDead = false;
        rb.simulated = true;
    }
}
