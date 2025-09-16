using System.Collections.Generic;
using UnityEngine;

public class Gollux_Controller : Boss_Controller
{
    // Components
    private Gollux gollux;
    private Boss_Health health;
    private Gollux_SkillManager skillManager;


    // Combos
    private List<WeightedCommand> longRangeRandoms = new(); // Long range commands to random
    private List<WeightedCommand> closeRangeRandoms = new(); // Close range commands to random



    protected override void Awake()
    {
        base.Awake();

        gollux = GetComponent<Gollux>();
        health = GetComponent<Boss_Health>();
        skillManager = GetComponent<Gollux_SkillManager>();

        SetRandomCommands();
    }

    protected override void DecideNextAction()
    {
        if (!gollux.isActivity || health.isDead || !canDecide)
            return;

        Boss_Command nextCommand = null;
        if (health.GetHealthPercent() <= 0.5f && skillManager.summon.canSummon)
        {
            nextCommand = gollux.summonCommand;

            // After timer can add HealCommand
            Invoke(nameof(AddHealCommand), skillManager.summon.GetHealTimer());
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
        longRangeRandoms.Add(new WeightedCommand(gollux.moveCommand, 30f));
        longRangeRandoms.Add(new WeightedCommand(gollux.rockDropCommand, 70f));

        // Close range commands
        closeRangeRandoms.Add(new WeightedCommand(gollux.normalAttackCommand, 70f));
        longRangeRandoms.Add(new WeightedCommand(gollux.rockDropCommand, 30f));
    }

    /// <summary>
    /// Can add HealCommand if have Enemy_Summon (canHeal)
    /// </summary>
    private void AddHealCommand()
    {
        if (skillManager.summon.CanHeal())
        {
            commandManager.AddCommand(gollux.healCommand);
        }
    }

    public override void AddFreezedCommand(float duration)
    {
        commandManager.AddCommand(new Boss_FreezedCommand(gollux, EParamenter_Boss.isFreezed.ToString(), duration));
    }
}
