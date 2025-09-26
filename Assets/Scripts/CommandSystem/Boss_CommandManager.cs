using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CommandManager : MonoBehaviour
{
    private Queue<Boss_Command> commands;
    private Boss_Command currentCommand;


    private bool isTrigger;
    private float commandTimer;


    private void Awake()
    {
        commands = new Queue<Boss_Command>();
    }

    private void Update()
    {
        commandTimer -= Time.deltaTime;

        if (commands.Count > 0 && currentCommand == null)
        {
            currentCommand = commands.Dequeue();

            if (currentCommand != null)
            {
                isTrigger = false;
                commandTimer = currentCommand.executeTime <= 0 ? float.MaxValue : currentCommand.executeTime;

                currentCommand.Execute();
            }
        }

        if (CanStopCommand())
            StopCurrentCommand();
    }

    public void AddCommand(Boss_Command command)
    {
        commands.Enqueue(command);
    }

    public void ClearCommands()
    {
        commands.Clear();
    }

    public void StopCurrentCommand()
    {
        if (currentCommand != null)
        {
            currentCommand.Undo();
            currentCommand = null;
        }
    }
    private bool CanStopCommand()
    {
        return currentCommand != null
            && (commandTimer <= 0 || (isTrigger && currentCommand.executeTime <= 0));
    }

    public void CallTrigger()
    {
        isTrigger = true;
    }
}
