using UnityEngine;

public class PlayerState : EntityState
{
    public Player player;

    public PlayerState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        this.player = player;

        rb = player.rb;
        anim = player.anim;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.wallDetect)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }
}
