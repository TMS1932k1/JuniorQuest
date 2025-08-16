using UnityEngine;

public class EntityState
{
    protected string nameState;
    protected StateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool isTrigger;
    protected float stateTimer;

    public EntityState(string nameState, StateMachine stateMachine, Player player)
    {
        this.nameState = nameState;
        this.stateMachine = stateMachine;
        this.player = player;

        rb = player.rb;
        anim = player.anim;
    }

    /// <summary>
    /// Set (true) to parameters of anim with (nameState)
    /// </summary>
    public virtual void Enter()
    {
        player.anim.SetBool(nameState, true);
    }

    /// <summary>
    /// Hanlde logic of state
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.wallDetect)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    /// <summary>
    /// Set (false) to parameters of anim with (nameState)
    /// </summary>
    public virtual void Exit()
    {
        player.anim.SetBool(nameState, false);
    }

    /// <summary>
    /// Reset velocity, not allow movement in new state
    /// </summary>
    protected void StopMoving()
    {
        player.SetVelocity(0, 0);
    }

    public virtual void CallTrigger()
    {
        isTrigger = true;
    }
}
