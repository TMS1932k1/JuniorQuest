using System.Collections.Generic;

public class Gollux_Controller : Boss_Controller
{
    private Gollux gollux;
    private Boss_Health health;
    private List<WeightedCommand> longRangeRandoms = new(); // Long range commands to random
    private List<WeightedCommand> closeRangeRandoms = new(); // Close range commands to random


    protected override void Awake()
    {
        base.Awake();

        gollux = GetComponent<Gollux>();
        health = GetComponent<Boss_Health>();

        SetRandomCommands();
    }


    protected override void DecideNextAction()
    {
        if (!gollux.isActivity && health.isDead)
            return;

        // Long range commands
        if (gollux.GetDisToTarget() > gollux.closeAttackDistance)
        {
            ICommand nextCommand = GetRandomCommand(longRangeRandoms);

            if (nextCommand != null)
                commandManager.AddCommand(nextCommand);
        }
    }

    private void SetRandomCommands()
    {
        longRangeRandoms.Add(new WeightedCommand(new Gollux_MoveCommand(gollux), 20f));
        longRangeRandoms.Add(new WeightedCommand(new Gollux_Attack1Command(gollux), 80f));
    }
}
