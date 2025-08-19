using UnityEngine;

public class Player_Combat : Entity_Combat
{
    private Player player;
    Collider2D[] counterTarget;

    [Header("Counter details")]
    [SerializeField] LayerMask whatIsCounter;
    public float counterDuration;
    public Transform counterCheckVelocity;
    public float counterCheckRadius;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    /// <summary>
    /// Get all targets which can be counter with (whatIsCounter) and (IsCanCounter), in (CounterCircles)
    /// </summary>
    /// <returns></returns>
    public bool HaveCounterTarget()
    {
        bool haveCounterTarget = false;

        counterTarget = Physics2D.OverlapCircleAll(counterCheckVelocity.position, counterCheckRadius, whatIsCounter);
        foreach (Collider2D target in counterTarget)
        {
            IsCanCounter canCounter = target.GetComponent<IsCanCounter>();
            if (canCounter != null && canCounter.GetCanCounter)
            {
                canCounter.HandleCounter();
                haveCounterTarget = true;
            }
        }

        return haveCounterTarget;
    }

    void OnDrawGizmos()
    {
        // Circle Counter
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(counterCheckVelocity.position, counterCheckRadius);
    }
}
