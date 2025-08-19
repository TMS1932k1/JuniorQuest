using UnityEngine;

public class Player_CounterState : PlayerState
{
    private Player_Combat playerCombat;
    private bool isCountered;

    public Player_CounterState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        playerCombat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = playerCombat.counterDuration;
        isCountered = false;
        isTrigger = false;
    }

    public override void Update()
    {
        base.Update();
        bool haveTargers = playerCombat.HaveCounterTarget();

        if (haveTargers && !isCountered)
        {
            anim.SetBool("isSuccessCounter", true);
            isCountered = true;
        }

        if (stateTimer < 0 && !haveTargers && !isCountered)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (isTrigger)
        {
            anim.SetBool("isSuccessCounter", false);
            stateMachine.ChangeState(player.idleState);
        }
    }
}
