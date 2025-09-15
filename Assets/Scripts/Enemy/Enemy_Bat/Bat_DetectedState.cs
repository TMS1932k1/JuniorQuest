using UnityEngine;

public class Bat_DetectedState : Enemy_DetectedState
{
    private Bat bat;

    public Bat_DetectedState(string nameState, StateMachine stateMachine, Bat bat) : base(nameState, stateMachine, bat)
    {
        this.bat = bat;
    }

    public override void Update()
    {
        base.Update();

        // Flip dir to player
        if (GetDirectToPlayer() != bat.faceDir)
            bat.Flip();

        if (enemy.isAttack)
        {
            //Change attack state
            stateMachine.ChangeState(bat.attackState);

        }
        else if (playerTransform != null)
        {
            float deltaSpeed = bat.moveDetectedSpeed * Time.deltaTime;
            // Move dir to player
            rb.MovePosition(Vector2.MoveTowards(bat.transform.position, playerTransform.position, deltaSpeed));
        }
    }
}
