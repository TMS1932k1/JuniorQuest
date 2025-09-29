using UnityEngine;

public class Player_AiredState : PlayerState
{
    protected static float lastDashPress;


    public Player_AiredState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState == player.dashState)
            return;

        anim.SetFloat(PlayerAnimationStrings.Y_VELOCITY_PARAM, rb.linearVelocityY);

        // Move on air
        if (Input.GetAxis("Horizontal") != 0)
        {
            player.SetVelocity(Input.GetAxisRaw("Horizontal") * player.moveSpeed * player.airMoveMultiplier, rb.linearVelocityY);
        }

        // Change DashState
        if (input.Player.Dash.WasPressedThisFrame() && !player.wallDetect && !player.isDead && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    private bool CanDash()
    {
        return Time.time > lastDashPress + player.dashCooldown;
    }
}
