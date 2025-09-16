using System.Collections.Generic;
using UnityEngine;

public class Gollux_Controller : Boss_Controller
{
    // Components
    private Gollux gollux;
    private Boss_Health bossHealth;
    private Gollux_SkillManager golluxSkillManager;


    // Combos
    private List<WeightedCommand> longRangeRandoms = new(); // Long range commands to random
    private List<WeightedCommand> closeRangeRandoms = new(); // Close range commands to random


    protected override void Awake()
    {
        base.Awake();

        gollux = GetComponent<Gollux>();
        bossHealth = GetComponent<Boss_Health>();
        golluxSkillManager = GetComponent<Gollux_SkillManager>();

        SetRandomCommands();
    }

    protected override void DecideNextAction()
    {
        if (!gollux.isActivity || bossHealth.isDead || !canDecide)
            return;

        Boss_Command nextCommand = null;
        if (bossHealth.GetHealthPercent() <= 0.5f && golluxSkillManager.summon.canSummon)
        {
            nextCommand = gollux.summonCommand;

            // After timer can add HealCommand
            Invoke(nameof(AddHealCommand), golluxSkillManager.summon.GetHealTimer());
        }
        else
        {
            nextCommand = gollux.GetDisToTarget() > gollux.closeAttackDistance ?
                GetRandomCommand(longRangeRandoms) :
                GetRandomCommand(closeRangeRandoms);
        }

        if (nextCommand != null)
            bossCommandManager.AddCommand(nextCommand);
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
        if (golluxSkillManager.summon.CanHeal())
        {
            bossCommandManager.AddCommand(gollux.healCommand);
        }
    }

    public override void AddFreezedCommand(float duration)
    {
        bossCommandManager.AddCommand(new Gollux_FreezedCommand(gollux, EParamenter_Boss.isFreezed.ToString(), duration));
    }

    public override void AddDeathCommand()
    {
        bossCommandManager.AddCommand(new Gollux_DeathCommand(gollux, EParamenter_Boss.isDeath.ToString()));
    }
}
