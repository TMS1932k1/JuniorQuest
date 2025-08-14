using UnityEngine;

public class Player_AiredState : EntityState
{
    public Player_AiredState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("yVelocity", rb.linearVelocityY);

        // Move on air
        if (Input.GetAxis("Horizontal") != 0)
        {
            player.SetVelocity(Input.GetAxisRaw("Horizontal") * player.moveSpeed * player.airMoveMultiplier, rb.linearVelocityY);
        }
    }
}
