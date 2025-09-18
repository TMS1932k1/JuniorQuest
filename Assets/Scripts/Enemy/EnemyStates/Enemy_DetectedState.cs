using Unity.VisualScripting;
using UnityEngine;

public class Enemy_DetectedState : EnemyState
{
    protected Transform playerTransform;
    private float lastDetectTime;


    public Enemy_DetectedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Get continuously transform of player
        if (playerTransform == null)
            playerTransform = enemy.DetectPlayer()?.transform;

        if (playerTransform == null)
        {
            playerTransform = enemy.GetComponent<Enemy_Health>().damageTransform;
            lastDetectTime = Time.time;
        }
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat(EnemyAnimationStrings.xVelocityParam, rb.linearVelocityX);

        UpdateTimeout();

        if (OvertimeDetect() || enemy.wallDetect)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (!enemy.isAttack)
            playerTransform = null;
    }

    /// <summary>
    /// Update timeout when player out distance of detector
    /// </summary>
    private void UpdateTimeout()
    {
        if (enemy.DetectPlayer())
        {
            lastDetectTime = Time.time;
        }
    }

    private bool OvertimeDetect()
    {
        return Time.time - lastDetectTime > enemy.durationDetect;
    }

    protected int GetDirectToPlayer()
    {
        if (playerTransform == null)
            return 0;

        return playerTransform.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
