using UnityEngine;

public class Bat : Enemy_Fly
{
    private Bat_SleepState sleepState;

    protected override void Awake()
    {
        base.Awake();

        idQuestTarget = IdQuestTargetStrings.ID_BAT;

        idleState = new Enemy_IdleState(EnemyAnimationStrings.IDLE_ANIM, stateMachine, this);
        moveState = new Enemy_FlyMoveState(EnemyAnimationStrings.MOVE_ANIM, stateMachine, this);
        detectedState = new Bat_DetectedState(EnemyAnimationStrings.DETECTED_ANIM, stateMachine, this);
        attackState = new Enemy_AttackState(EnemyAnimationStrings.ATTACK_ANIM, stateMachine, this);
        deathState = new Enemy_DeathState(EnemyAnimationStrings.DEATH_ANIM, stateMachine, this);
        freezedState = new Enemy_FreezedState(EnemyAnimationStrings.FREEZED_ANIM, stateMachine, this);
        stunnedState = new Enemy_StunnedState(EnemyAnimationStrings.STUNNED_ANIM, stateMachine, this);
        sleepState = new Bat_SleepState(BatAnimationStrings.SLEEP_ANIM, stateMachine, this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(sleepState);
    }

    protected override void HandleCollisions()
    {
        isAttack = Physics2D.Raycast(transform.position, Vector2.right * faceDir, distanceToAttack, whatIsPlayer);
    }

    public override void LoadData(GameData gameData)
    {
        base.LoadData(gameData);

        if (gameData.entities.ContainsKey(uniqueId) && !gameData.entities[uniqueId])
            stateMachine.ChangeState(sleepState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // Line detect player to attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(distanceToAttack * faceDir, 0, 0));
    }
}
