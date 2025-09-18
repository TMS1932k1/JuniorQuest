using UnityEngine;

public class Bat_SleepState : EnemyState
{
    private UI_HealthBar healthBarUI;
    private bool isWaked;

    public Bat_SleepState(string nameState, StateMachine stateMachine, Enemy enemy) : base(nameState, stateMachine, enemy)
    {
        healthBarUI = enemy.GetComponentInChildren<UI_HealthBar>();
    }

    public override void Enter()
    {
        base.Enter();

        isWaked = false;
        healthBarUI.gameObject.SetActive(false);
        enemy.gameObject.layer = LayerMask.NameToLayer(LayerStrings.InvisibilityLayer);
    }

    public override void Update()
    {
        base.Update();

        if (isTrigger)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (enemy.DetectPlayer() && !isWaked)
        {
            anim.SetTrigger(BatAnimationStrings.wakeUpTrigger);
            isWaked = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        healthBarUI.gameObject.SetActive(true);
        enemy.gameObject.layer = LayerMask.NameToLayer(LayerStrings.EnemyLayer);
    }
}
