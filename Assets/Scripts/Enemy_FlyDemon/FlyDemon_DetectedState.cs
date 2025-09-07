using UnityEngine;

public class FlyDemon_DetectedState : Enemy_DetectedState
{
    private FlyDemon flyDemon;
    private Entity_RangedCombat entityRangedCombat;
    private Transform targetPoint;


    public FlyDemon_DetectedState(string nameState, StateMachine stateMachine, FlyDemon flyDemon) : base(nameState, stateMachine, flyDemon)
    {
        this.flyDemon = flyDemon;
        entityRangedCombat = flyDemon.GetComponent<Entity_RangedCombat>();
    }

    public override void Update()
    {
        base.Update();

        // Flip dir to player
        if (GetDirectToPlayer() != flyDemon.faceDir)
            flyDemon.Flip();

        if (enemy.isAttack && !entityRangedCombat.isRangdAttack)
        {
            //Change attack state
            stateMachine.ChangeState(flyDemon.attackState);
        }
        else
        {
            // Move near to player
            if (playerTransform != null)
            {
                targetPoint = GetNearestPoint();
                MoveToPoint(targetPoint);
            }
        }

    }

    private void MoveToPoint(Transform targetPoint)
    {
        float deltaSpeed = flyDemon.moveDetectedSpeed * Time.deltaTime;
        rb.MovePosition(Vector2.MoveTowards(flyDemon.transform.position, targetPoint.position, deltaSpeed));
    }

    /// <summary>
    /// Find nearest point to player
    /// </summary>
    /// <param name="index">Return index of Transform in points list</param>
    /// <returns>Nearest point</returns>
    private Transform GetNearestPoint()
    {
        Transform nearestPoint = null;
        float minDis = float.MaxValue;

        foreach (Transform point in flyDemon.flyLine)
        {
            float dis = Vector2.Distance(playerTransform.position, point.position);

            if (dis < minDis)
            {
                minDis = dis;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }
}
