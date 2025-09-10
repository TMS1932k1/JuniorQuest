using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gollux : MonoBehaviour
{
    [Header("Move details")]
    [SerializeField] float moveSpeed;


    [Header("Activity Arena details")]
    [SerializeField] Transform arenaTrans;
    [SerializeField] float widthArena;
    [SerializeField] float heightArena;
    [SerializeField] LayerMask whatIsDetect;
    public Transform playerTrans { get; private set; }


    // Components
    private Animator anim;


    private Queue<ICommand> commands = new();
    private bool isExecuting = false;
    private float delayCommands = 1f;

    private Coroutine MoveCoroutine;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        playerTrans = null;
    }

    void Update()
    {
        if (!isExecuting && commands.Count > 0)
        {
            ICommand command = commands.Dequeue();
            StartCoroutine(ExecuteCommand(command));
        }

        HandleDetectPlayer();
    }

    public void AddCommand(ICommand command)
    {
        commands.Enqueue(command);
    }

    private IEnumerator ExecuteCommand(ICommand command)
    {
        isExecuting = true;
        command.Execute(this);

        yield return new WaitForSeconds(delayCommands);

        isExecuting = false;
    }

    /// <summary>
    /// Move to target position
    /// </summary>
    /// <param name="targetPos">Position to goal</param>
    public void MoveTo(Vector3 targetPos)
    {
        if (MoveCoroutine != null)
            StopCoroutine(MoveCoroutine);

        MoveCoroutine = StartCoroutine(MoveCo(targetPos));
    }

    private IEnumerator MoveCo(Vector3 targetPos)
    {
        if (IsInsideArena(targetPos))
        {
            anim.Play("Gollux_Move");
            while (Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            anim.Play("Gollux_Idle");
        }
    }

    /// <summary>
    /// Check pos is inside activity arena
    /// </summary>
    /// <param name="pos">Positon need check</param>
    /// <returns>true = inside, false = outside</returns>
    public bool IsInsideArena(Vector3 pos)
    {
        return true;
    }

    private Collider2D DetectInActivityArena()
    {
        return Physics2D.OverlapBox(arenaTrans.position, new Vector2(widthArena, heightArena), whatIsDetect);
    }

    private void HandleDetectPlayer()
    {
        Collider2D col = DetectInActivityArena();
        playerTrans = col?.gameObject.transform;
        Debug.Log(playerTrans.position.x);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(arenaTrans.position, new Vector2(widthArena, heightArena));
    }
}
