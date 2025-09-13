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


    private Entity_Stat stat;
    private Boss_CommandManager commandManager;
    private Boss_Controller controller;


    protected override void Awake()
    {
        base.Awake();

        stat = GetComponent<Entity_Stat>();
        commandManager = GetComponent<Boss_CommandManager>();
        controller = GetComponent<Boss_Controller>();
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }

    protected override void Update()
    {
        // Don't need (stateMachine) and (HandleCollisions) => base.Update() isn't important
        // base.Update();

        DetectTarget();

        isActivity = detectTarget != null; // Activity when player in arena
    }

    public override void OnDead()
    {
        base.OnDead();

        OnBossDeath.Invoke(stat.GetXp());
    }

    public void HandlePlayerDeath()
    {
        controller.OffDecideAction();
        commandManager.ClearCommands();
    }

    private void DetectTarget()
    {
        detectTarget = GetTargetInArena();
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
