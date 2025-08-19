using UnityEngine;

public class PlayerState : EntityState
{
    public Player player;

    private float lastDashPress;

    public PlayerState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        this.player = player;

        rb = player.rb;
        anim = player.anim;
    }

    public override void Update()
    {
        base.Update();

        // Change DashState
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.wallDetect && !player.isDead && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
            lastDashPress = Time.time;
        }
    }

    private bool CanDash()
    {
        return Time.time > lastDashPress + player.dashColdown
            && stateMachine.currentState != player.counterState;
    }
}
