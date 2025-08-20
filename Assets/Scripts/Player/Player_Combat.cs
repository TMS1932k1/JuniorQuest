using UnityEngine;

public class Player_Combat : Entity_Combat
{
    private Player player;


    [Header("Counter details")]
    [SerializeField] LayerMask whatIsCounter;
    public float counterDuration;
    public Transform counterCheckVelocity;
    public float counterCheckRadius;
    Collider2D[] counterTarget;

    protected override void Awake()
    {
        base.Awake();

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
                entityVFX.CreateHitVFX(canCounter.GetTransform.position);
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
