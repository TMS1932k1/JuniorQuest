using UnityEngine;

public class Player_FallState : Player_AiredState
{
    private bool isJumpTwo;

    public Player_FallState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.wallDetect && !player.groundDetect)
        {
            isJumpTwo = false;
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (player.groundDetect)
        {
            playerSFX.PlayLand();

            isJumpTwo = false;
            stateMachine.ChangeState(player.idleState);
        }

        // Change JumpState (Jump Two)
        if (Input.GetKeyDown(KeyCode.Space) && !isJumpTwo)
        {
            isJumpTwo = true;
            anim.SetTrigger(PlayerAnimationStrings.JUMP_TWO_TRIGGER);

            stateMachine.ChangeState(player.jumpState);
        }
    }
}
