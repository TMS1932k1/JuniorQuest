using Unity.VisualScripting;
using UnityEngine;

public class Enemy_PlayerDetectedState : EnemyState
{
    private Transform playerTransform;
    private float lastPlayerDetectTime;

    public Enemy_PlayerDetectedState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Get continuously transform of player
        if (playerTransform == null)
            playerTransform = enemy.DetectPlayer().transform;
    }

    public override void Update()
    {
        base.Update();

        UpdateTimeout();

        if (OvertimeDetect() || enemy.wallDetect)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (enemy.isAttack)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.SetVelocity(enemy.moveDetectedSpeed * GetDirectToPlayer(), rb.linearVelocityY);
        }
    }

    /// <summary>
    /// Update timeout when player out distance of detector
    /// </summary>
    private void UpdateTimeout()
    {
        if (enemy.DetectPlayer())
        {
            lastPlayerDetectTime = Time.time;
        }
    }

    private bool OvertimeDetect()
    {
        return Time.time - lastPlayerDetectTime > enemy.durationDetect;
    }

    private int GetDirectToPlayer()
    {
        if (playerTransform == null)
            return 0;

        return playerTransform.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
