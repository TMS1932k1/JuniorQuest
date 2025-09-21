using System;
using System.Collections;
using UnityEngine;

public class Boss : Entity, ISaveable
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
    private Entity_Stat entityStat;
    private Boss_Health bossHealth;
    private Boss_CommandManager bossCommandManager;
    private Boss_Controller bossController;
    private Boss_Inventory bossInventory;
    protected SpriteRenderer sr;


    protected override void Awake()
    {
        base.Awake();

        entityStat = GetComponent<Entity_Stat>();
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

        OnBossDeath.Invoke(entityStat.GetXp());
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

    public void SaveData(ref GameData gameData)
    {
        if (!gameData.entities.ContainsKey(uniqueId))
        {
            Debug.Log($"SAVE_MANAGER: Save {gameObject.name} ({uniqueId})");
            gameData.entities.Add(uniqueId, bossHealth.isDead);
        }
        else
        {
            Debug.Log($"SAVE_MANAGER: Update {gameObject.name} ({uniqueId})");
            gameData.entities[uniqueId] = bossHealth.isDead;
        }
    }

    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load {gameObject.name} ({uniqueId})");
        if (!gameData.entities.ContainsKey(uniqueId) || gameData.entities[uniqueId])
        {
            bossHealth.isDead = true;

            StopCommandSystem();
            bossController.AddDeathCommand();
        }
    }

    protected override void OnDrawGizmos()
    {
        // Activity Arena
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(arenaTrans.position, new Vector2(widthArena, heightArena));
    }
}
