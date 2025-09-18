using UnityEngine;


public class PlayerAnimationStrings
{
    public const string IDLE_ANIM = "isIdle";
    public const string MOVE_ANIM = "isMove";
    public const string JUMP_FALL_ANIM = "isJumpFall";
    public const string Y_VELOCITY_PARAM = "yVelocity";
    public const string WALL_SLIDE_ANIM = "isWallSlide";
    public const string WALL_JUMP_ANIM = "isWallJump";
    public const string DASH_ANIM = "isDash";
    public const string ATTACK_ANIM = "isAttack";
    public const string ATTACK_INDEX_PARAM = "attackIndex";
    public const string ATTACK_END_TRIGGER = "attackEnd";
    public const string SLIDE_ANIM = "isSlide";
    public const string HURT_ANIM = "isHurt";
    public const string DEATH_ANIM = "isDeath";
    public const string COUNTER_ANIM = "isCounter";
    public const string SUCCESS_COUNTER_ANIM = "isSuccessCounter";
    public const string FIRE_BLADE_ANIM = "isFireBlade";
    public const string FIRE_BLADE_TRIGGER = "fireBlade";
    public const string JUMP_TWO_TRIGGER = "jumpTwo";
    public const string HIT_TRIGGER = "hit";
}


public class LayerStrings
{
    public const string GROUND_LAYER = "Ground";
    public const string PLAYER_LAYER = "Player";
    public const string BACKGROUND_LAYER = "Background";
    public const string ENEMY_LAYER = "Enemy";
    public const string INTERACTABLE_LAYER = "Interactable";
    public const string INVISIBILITY_LAYER = "Invisibility";
    public const string ENEMY_ATTACK_LAYER = "EnemyAttack";
    public const string PLAYER_ATTACK_LAYER = "PlayerAttack";
    public const string HAZARD_LAYER = "Hazard";
    public const string ELEVATOR_LAYER = "Elevator";
    public const string BREAKABLE_LAYER = "Breakable";
}


public class BossAnimationStrings
{
    public const string IDLE_ANIM = "isIdle";
    public const string MOVE_ANIM = "isMove";
    public const string FREEZED_ANIM = "isFreezed";
    public const string DEATH_ANIM = "isDeath";
}


public class GolluxAnimationStrings : BossAnimationStrings
{
    public const string NORMAL_ATTACK_ANIM = "isNormalAttack";
    public const string ROCK_DROP_ANIM = "isRockDrop";
    public const string SUMMON_ANIM = "isSummon";
    public const string HEAL_ANIM = "isHeal";
}


public class EnemyAnimationStrings
{
    public const string IDLE_ANIM = "isIdle";
    public const string MOVE_ANIM = "isMove";
    public const string ATTACK_ANIM = "isAttack";
    public const string DETECTED_ANIM = "isDetected";
    public const string SPEED_MUTIPLIER_PARAM = "speedMutiplier";
    public const string X_VELOCITY_PARAM = "xVelocity";
    public const string DEATH_ANIM = "isDeath";
    public const string STUNNED_ANIM = "isStunned";
    public const string FREEZED_ANIM = "isFreezed";
    public const string HIT_TRIGGER = "hit";
}


public class BatAnimationStrings : EnemyAnimationStrings
{
    public const string SLEEP_ANIM = "isSleep";
    public const string WAKE_UP_TRIGGER = "wakeUp";
}


public class SummonAnimationStrings : EnemyAnimationStrings
{
    public const string SUMMON_ANIM = "isSummon";
    public const string DISMISS_ANIM = "isDismiss";
}


public class SourceStatStrings
{
    public const string POINT_SOURCE = "Point";
}