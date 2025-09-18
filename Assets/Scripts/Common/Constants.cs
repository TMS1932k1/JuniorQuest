using UnityEngine;


public class PlayerAnimationStrings
{
    public const string idleAnim = "isIdle";
    public const string moveAnim = "isMove";
    public const string jumpFallAnim = "isJumpFall";
    public const string yVelocityParam = "yVelocity";
    public const string wallSlideAnim = "isWallSlide";
    public const string wallJumpAnim = "isWallJump";
    public const string dashAnim = "isDash";
    public const string attackAnim = "isAttack";
    public const string attackIndexParam = "attackIndex";
    public const string attackEndTrigger = "attackEnd";
    public const string slideAnim = "isSlide";
    public const string hurtAnim = "isHurt";
    public const string deathAnim = "isDeath";
    public const string counterAnim = "isCounter";
    public const string successCounterAnim = "isSuccessCounter";
    public const string fireBladeAnim = "isFireBlade";
    public const string fireBladeTrigger = "fireBlade";
    public const string jumpTwoTrigger = "jumpTwo";
    public const string hitTrigger = "hit";
}


public class LayerStrings
{
    public const string GroundLayer = "Ground";
    public const string PlayerLayer = "Player";
    public const string BackgroundLayer = "Background";
    public const string EnemyLayer = "Enemy";
    public const string InteractableLayer = "Interactable";
    public const string InvisibilityLayer = "Invisibility";
    public const string EnemyAttackLayer = "EnemyAttack";
    public const string PlayerAttackLayer = "PlayerAttack";
    public const string HazardLayer = "Hazard";
    public const string ElevatorLayer = "Elevator";
    public const string BreakableLayer = "Breakable";
}


public class BossAnimationStrings
{
    public const string idleAnim = "isIdle";
    public const string moveAnim = "isMove";
    public const string freezedAnim = "isFreezed";
    public const string deathAnim = "isDeath";
}


public class GolluxAnimationStrings : BossAnimationStrings
{
    public const string normalAttackAnim = "isNormalAttack";
    public const string rockDropAnim = "isRockDrop";
    public const string summonAnim = "isSummon";
    public const string healAnim = "isHeal";
}


public class EnemyAnimationStrings
{
    public const string idleAnim = "isIdle";
    public const string moveAnim = "isMove";
    public const string attackAnim = "isAttack";
    public const string detectedAnim = "isDetected";
    public const string speedMutiplierParam = "speedMutiplier";
    public const string xVelocityParam = "xVelocity";
    public const string deathAnim = "isDeath";
    public const string stunnedAnim = "isStunned";
    public const string freezedAnim = "isFreezed";
    public const string hitTrigger = "hit";
}


public class BatAnimationStrings : EnemyAnimationStrings
{
    public const string sleepAnim = "isSleep";
    public const string wakeUpTrigger = "wakeUp";
}

public class SummonAnimationStrings : EnemyAnimationStrings
{
    public const string summonAnim = "isSummon";
    public const string dismissAnim = "isDismiss";
}