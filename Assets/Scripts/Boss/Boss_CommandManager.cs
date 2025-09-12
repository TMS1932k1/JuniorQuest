using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CommandManager : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float delayExecute = 2f;
    private Queue<ICommand> commands;
    private bool isExecuting;
    private Coroutine executeCoroutine;


    private void Awake()
    {
        commands = new Queue<ICommand>();
    }

    private void Start()
    {
        isExecuting = false;
    }

    public void AddCommand(ICommand command)
    {
        commands.Enqueue(command);
    }

    private void Update()
    {
        if (commands.Count > 0 && !isExecuting)
        {
            ICommand nextCommand = commands.Dequeue();

            if (executeCoroutine != null)
                StopCoroutine(executeCoroutine);

            executeCoroutine = StartCoroutine(ExecuteCommand(nextCommand));
        }
    }

    private IEnumerator ExecuteCommand(ICommand command)
    {
        isExecuting = true;
        command.Execute();

        yield return new WaitForSeconds(delayExecute);

        command.Undo();
        isExecuting = false;
    }
}
