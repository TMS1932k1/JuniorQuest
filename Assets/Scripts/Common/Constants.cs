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


public class ClipDataNameStrings
{
    // Player
    public const string PLAYER_ATTACK_HIT = "player_attack_hit";
    public const string PLAYER_ATTACK_MISS = "player_attack_miss";
    public const string PLAYER_JUMP = "player_jump";
    public const string PLAYER_LAND = "player_land";
    public const string PLAYER_DASH = "player_dash";
    public const string PLAYER_SLIDE = "player_slide";
    public const string PLAYER_COUNTER = "player_counter";
    public const string PLAYER_BUFF = "player_buff";
    public const string PLAYER_FIRE_BLADE = "player_fire_blade";
    public const string PLAYER_INVISIBILITY = "player_invisibility";
    public const string PLAYER_COMEBACK = "player_comeback";
    public const string PLAYER_BATTLE_CRY = "player_battle_cry";
    public const string PLAYER_ICE_PRISON = "player_ice_prison";
    public const string PLAYER_SHIELD_BARRIER = "player_shield_barrier";
    public const string PLAYER_MOVE = "player_move";
    public const string PLAYER_LEVEL_UP = "player_level_up";
    public const string PLAYER_DEATH = "player_death";
    public const string PLAYER_HURT = "player_hurt";
    public const string PLAYER_INFENO = "player_infeno";
    public const string PLAYER_PICK_UP = "player_pick_up";


    // Golem
    public const string GOLEM_ATTACK_HIT = "golem_attack_hit";
    public const string GOLEM_ATTACK_MISS = "golem_attack_miss";
    public const string GOLEM_DEATH = "golem_death";


    // Bat
    public const string BAT_ATTACK_HIT = "bat_attack_hit";
    public const string BAT_ATTACK_MISS = "bat_attack_miss";
    public const string BAT_DEATH = "bat_death";


    // Fly Demon
    public const string FLY_DEMON_DEATH = "fly_demon_death";


    // Gollux
    public const string GOLLUX_ATTACK_HIT = "gollux_attack_hit";
    public const string GOLLUX_ATTACK_MISS = "gollux_attack_miss";
    public const string GOLLUX_DEATH = "gollux_death";


    // Range attack
    public const string RANGE_ATTACK_HIT = "range_attack_hit";


    // Elevator
    public const string ELEVATOR_ACTIVITY = "elevator_avtivity";


    // Checkpoint
    public const string CHECKPOINT_SAVE = "checkpoint_save";


    // UI
    public const string UI_CONFIRM = "ui_confirm";
    public const string UI_DENIED = "ui_denied";
    public const string UI_EQUIP = "ui_equip";
    public const string UI_HOVER = "ui_hover";
    public const string UI_PAUSE = "ui_pause";
    public const string UI_UNEQUIP = "ui_unequip";
    public const string UI_UNPAUSE = "ui_unpause";
    public const string UI_DECIDE = "ui_decide";
}