using UnityEngine;

public class Player_FireBladeState : PlayerState
{
    private Player_VFX playerVFX;

    private bool isArming;

    public Player_FireBladeState(string nameState, StateMachine stateMachine, Player player) : base(nameState, stateMachine, player)
    {
        playerVFX = player.GetComponent<Player_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 3f; // Time out
        isTrigger = false;

        // Show VFX
        playerVFX.ShowFireBladeVFX();
        isArming = true;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);

        // Set rect transform of line arm and get positon of mouse
        playerVFX.SetLineArmRotate(out float angleZ);
        HanldeFlip(angleZ);

        // Perform Attack with dir to mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerSFX.PlayFireBlade();
            anim.SetTrigger(PlayerAnimationStrings.FIRE_BLADE_TRIGGER);

            // Create Fire Blade to target
            playerSkillsManager.fireBlade.CreateFireBlade(angleZ);
            isArming = false;
        }

        // Overtime or end skill
        if ((stateTimer <= 0 && isArming) || isTrigger)
        {
            stateMachine.ChangeState(player.idleState);
            isArming = false;
        }

        // Off VFX
        if (!isArming)
            playerVFX.HideFireBladeVFX();
    }

    public override void Exit()
    {
        base.Exit();

        if (isArming)
            playerVFX.HideFireBladeVFX();
    }

    private void HanldeFlip(float angleZ)
    {
        if (angleZ <= 90 && player.faceDir == -1)
        {
            player.Flip();
        }
        else if (angleZ > 90 && player.faceDir == 1)
        {
            player.Flip();
        }
    }
}
