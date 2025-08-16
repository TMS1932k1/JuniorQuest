using UnityEngine;

public abstract class EntityState
{
    protected string nameState;
    protected StateMachine stateMachine;
    protected Entity entity;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool isTrigger;
    protected float stateTimer;

    public EntityState(string nameState, StateMachine stateMachine, Entity entity)
    {
        this.nameState = nameState;
        this.stateMachine = stateMachine;
        this.entity = entity;
    }

    /// <summary>
    /// Set (true) to parameters of anim with (nameState)
    /// </summary>
    public virtual void Enter()
    {
        entity.anim.SetBool(nameState, true);
    }

    /// <summary>
    /// Hanlde logic of state
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    /// <summary>
    /// Set (false) to parameters of anim with (nameState)
    /// </summary>
    public virtual void Exit()
    {
        entity.anim.SetBool(nameState, false);
    }

    /// <summary>
    /// Reset velocity, not allow movement in new state
    /// </summary>
    protected void StopMoving()
    {
        entity.SetVelocity(0, 0);
    }

    public virtual void CallTrigger()
    {
        isTrigger = true;
    }
}
