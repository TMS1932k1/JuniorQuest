using Unity.VisualScripting;
using UnityEngine;

public class Player_SlideState : PlayerState
{
    private CapsuleCollider2D col;
    private Vector2 originSizeCol;
    private Vector2 originOffsetCol;


    public Player_SlideState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        col = player.GetComponent<CapsuleCollider2D>();

        originSizeCol = col.size;
        originOffsetCol = col.offset;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.slideDuration;

        playerSFX.PlaySlide();

        // Set size Collider of SlideState
        col.size = new Vector2(originSizeCol.x, originSizeCol.y / 2);
        col.offset = new Vector2(originOffsetCol.x, originOffsetCol.y - 0.2f);
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

        // Set origin size Collider of IdleState
        col.size = originSizeCol;
        col.offset = originOffsetCol;
    }

    private bool CancleIfNeed()
    {
        if (player.wallDetect) return true;
        return false;
    }
}
