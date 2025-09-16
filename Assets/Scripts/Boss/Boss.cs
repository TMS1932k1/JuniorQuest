using System;
using System.Collections;
using UnityEngine;

public class Boss : Entity
{
    public static event Action<float> OnBossDeath;


    [Header("Active Arena")]
    [SerializeField] Transform arenaTrans;
    [SerializeField] float widthArena;
    [SerializeField] float heightArena;
    [SerializeField] LayerMask whatIsDetect;
    public Collider2D detectTarget { get; private set; }
    public bool isActivity { get; private set; }


    // Components
    private Entity_Stat stat;
    private Boss_CommandManager bossCommandManager;
    private Boss_Health bossHealth;
    private Boss_Controller bossController;
    private Boss_Inventory bossInventory;
    protected SpriteRenderer sr;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponent<Entity_Stat>();
        sr = GetComponentInChildren<SpriteRenderer>();
        bossCommandManager = GetComponent<Boss_CommandManager>();
        bossController = GetComponent<Boss_Controller>();
        bossHealth = GetComponent<Boss_Health>();
        bossInventory = GetComponent<Boss_Inventory>();
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += StopCommandSystem;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= StopCommandSystem;
    }

    protected override void Update()
    {
        // Don't need (stateMachine) and (HandleCollisions) => base.Update() isn't important
        // base.Update();

        DetectTarget();
        HandleFlip();

        isActivity = detectTarget != null; // Activity when player in arena
    }

    public override void OnDead()
    {
        base.OnDead();

        StopCommandSystem();
        bossController.AddDeathCommand();
        bossInventory.DropAllInventory();

        OnBossDeath.Invoke(stat.GetXp());
    }

    public override void BeFreezed(float duration)
    {
        StopCommandSystem(); // Stop all commands, then add (FreezedCommand) to execute
        bossController.AddFreezedCommand(duration);
    }

    public override void ExitFreezed()
    {
        bossController.EnableDecideAction(true);
    }

    private void StopCommandSystem()
    {
        bossController.EnableDecideAction(false);

        bossCommandManager.ClearCommands();
        bossCommandManager.StopCurrentCommand();
    }

    private void DetectTarget()
    {
        detectTarget = !bossHealth.isDead ? GetTargetInArena() : null;
    }

    private void HandleFlip()
    {
        if (detectTarget == null)
            return;

        if (transform.position.x < detectTarget.transform.position.x && faceDir != 1)
            Flip();

        if (transform.position.x > detectTarget.transform.position.x && faceDir != -1)
            Flip();
    }

    private Collider2D GetTargetInArena()
    {
        return Physics2D.OverlapBox(arenaTrans.position, new Vector2(widthArena, heightArena), 0, whatIsDetect);
    }

    protected override void OnDrawGizmos()
    {
        // Activity Arena
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(arenaTrans.position, new Vector2(widthArena, heightArena));
    }
}
