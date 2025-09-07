using UnityEngine;

public class Enemy_FlyMoveState : Enemy_MoveState
{
    private Enemy_Fly enemyFly;
    private int currentPoint;
    private bool isBackMove;

    public Enemy_FlyMoveState(string nameState, StateMachine stateMachine, Enemy_Fly enemyFly) : base(nameState, stateMachine, enemyFly)
    {
        this.enemyFly = enemyFly;
    }

    public override void Enter()
    {
        base.Enter();

        currentPoint = enemyFly.flyLine.Length - 1;
        isBackMove = false;
    }

    public override void Update()
    {
        base.Update();

        FlyWithLine();
        HandleFlip();
    }

    private void FlyWithLine()
    {
        Vector3 nextPoint = enemyFly.flyLine[currentPoint].position;
        float deltaSpeed = enemyFly.moveSpeed * Time.deltaTime;
        rb.MovePosition(Vector2.MoveTowards(enemyFly.transform.position, nextPoint, deltaSpeed));

        if (enemyFly.transform.position == nextPoint)
        {
            if (currentPoint >= enemyFly.flyLine.Length - 1)
                isBackMove = true;
            else if (currentPoint <= 0)
                isBackMove = false;

            if (isBackMove)
                currentPoint--;
            else
                currentPoint++;
        }
    }

    private void HandleFlip()
    {
        if (enemyFly.transform.position.x < enemyFly.flyLine[currentPoint].position.x && enemyFly.faceDir != 1)
            enemyFly.Flip();

        if (enemyFly.transform.position.x > enemyFly.flyLine[currentPoint].position.x && enemyFly.faceDir != -1)
            enemyFly.Flip();
    }
}
