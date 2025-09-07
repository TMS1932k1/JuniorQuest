using UnityEngine;

public class Enemy_Fly : Enemy
{
    [Header("Fly")]
    public Transform[] flyLine;

    /// <summary>
    /// Detect player with (distanceToDetectPlayer)
    /// if not detect then return (null)
    /// </summary>
    /// <returns></returns>
    public override Collider2D DetectPlayer()
    {
        playerDetect = Physics2D.OverlapCircle(transform.position, distanceToDetectPlayer, whatIsPlayer);
        return playerDetect;
    }

    protected override void OnDrawGizmos()
    {
        // Line detect player to attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToDetectPlayer);
    }
}
