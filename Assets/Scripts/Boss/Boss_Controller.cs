using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WeightedCommand
{
    public ICommand command;
    public float weight;

    public WeightedCommand(ICommand command, float weight)
    {
        this.command = command;
        this.weight = weight;
    }
}

public abstract class Boss_Controller : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] protected float delayDecide = 3f;


    protected Boss_CommandManager commandManager;


    protected virtual void Awake()
    {
        commandManager = GetComponent<Boss_CommandManager>();
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(DecideNextAction), 1, delayDecide);
    }

    protected abstract void DecideNextAction(); // Need override at child class to selbst decide next action

    /// <summary>
    /// Random command to perform with (Weight)
    /// </summary>
    /// <returns></returns>
    protected ICommand GetRandomCommand(List<WeightedCommand> commands)
    {
        float totalWeight = 0f;
        foreach (var wc in commands)
            totalWeight += wc.weight;

        float randomValue = Random.value * totalWeight;

        foreach (var wc in commands)
        {
            if (randomValue < wc.weight)
                return wc.command;

            randomValue -= wc.weight;
        }

        return null;
    }
}
