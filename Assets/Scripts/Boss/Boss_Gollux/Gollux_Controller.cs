using System.Collections.Generic;

public class Gollux_Controller : Boss_Controller
{
    private Gollux gollux;
    private Boss_Health health;


    private List<WeightedCommand> longRangeRandoms = new(); // Long range commands to random
    private List<WeightedCommand> closeRangeRandoms = new(); // Close range commands to random
    private bool canSummon = true;


    protected override void Awake()
    {
        base.Awake();

        gollux = GetComponent<Gollux>();
        health = GetComponent<Boss_Health>();

        SetRandomCommands();
    }

    protected override void DecideNextAction()
    {
        if (!gollux.isActivity || health.isDead || !canDecide)
            return;


        Boss_Command nextCommand = null;
        if (health.GetHealthPercent() <= 0.5f && canSummon)
        {
            canSummon = false;
            // Add command of Skill Summon Golem
        }
        else
        {
            nextCommand = gollux.GetDisToTarget() > gollux.closeAttackDistance ?
                GetRandomCommand(longRangeRandoms) :
                GetRandomCommand(closeRangeRandoms);
        }

        if (nextCommand != null)
            commandManager.AddCommand(nextCommand);
    }

    private void SetRandomCommands()
    {
        // Long range commands
        longRangeRandoms.Add(new WeightedCommand(new Gollux_MoveCommand(gollux, 2f), 30f));
        longRangeRandoms.Add(new WeightedCommand(new Gollux_RockDropCommand(gollux), 70f));

        // Close range commands
        closeRangeRandoms.Add(new WeightedCommand(new Gollux_NormalAttackCommand(gollux), 70f));
        longRangeRandoms.Add(new WeightedCommand(new Gollux_RockDropCommand(gollux), 30f));
    }
}
