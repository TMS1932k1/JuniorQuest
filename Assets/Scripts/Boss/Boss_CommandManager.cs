using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CommandManager : MonoBehaviour
{
    private Queue<Boss_Command> commands;
    private bool isExecuting;
    private bool isTrigger;
    private Coroutine executeCoroutine;


    private void Awake()
    {
        commands = new Queue<Boss_Command>();
    }

    private void Start()
    {
        isExecuting = false;
    }

    private void Update()
    {
        if (commands.Count > 0 && !isExecuting)
        {
            Boss_Command nextCommand = commands.Dequeue();

            if (executeCoroutine != null)
                StopCoroutine(executeCoroutine);

            executeCoroutine = StartCoroutine(ExecuteCommand(nextCommand));
        }
    }

    public void AddCommand(Boss_Command command)
    {
        commands.Enqueue(command);
    }

    public void ClearCommands()
    {
        commands.Clear();
    }

    private IEnumerator ExecuteCommand(Boss_Command command)
    {
        isExecuting = true;
        isTrigger = false;
        command.Execute();

        // Wait for execute time
        yield return new WaitForSeconds(command.executeTime);

        // If have not executeTime => wait end animation
        while (command.executeTime <= 0 && !isTrigger)
            yield return null;

        command.Undo();
        isExecuting = false;
    }

    public void CallTrigger()
    {
        Debug.Log("Call Trigger");
        isTrigger = true;
    }
}
