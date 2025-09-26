using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeightedCommand
{
    public Boss_Command bossCommand;
    public float weight;

    public WeightedCommand(Boss_Command command, float weight)
    {
        this.bossCommand = command;
        this.weight = weight;
    }
}

public abstract class Boss_Controller : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] protected float delayDecide = 3f;
    protected bool canDecide = true;


    protected Boss_CommandManager bossCommandManager;


    protected virtual void Awake()
    {
        bossCommandManager = GetComponent<Boss_CommandManager>();
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(DecideNextAction), 1, delayDecide);
    }

    public void EnableDecideAction(bool enable) => canDecide = enable;

    /// <summary>
    /// Random command to perform with (Weight)
    /// </summary>
    /// <returns></returns>
    protected Boss_Command GetRandomCommand(List<WeightedCommand> commands)
    {
        float totalWeight = 0f;
        foreach (var wc in commands)
            totalWeight += wc.weight;

        float randomValue = Random.value * totalWeight;

        foreach (var wc in commands)
        {
            if (randomValue < wc.weight)
                return wc.bossCommand;

            randomValue -= wc.weight;
        }

        return null;
    }

    protected abstract void DecideNextAction(); // Need override at child class to selbst decide next action
    public abstract void AddFreezedCommand(float duration);
    public abstract void AddDeathCommand();
}
