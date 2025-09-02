using System;
using UnityEngine;

public class Player_XP : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int level;
    [SerializeField] float currentXP;
    [SerializeField] float expMutiplier = 100f;

    private float maxXP;
    public bool newLevelUp = true;

    private Player_VFX playVFX;


    void Awake()
    {
        playVFX = GetComponent<Player_VFX>();
    }

    void Start()
    {
        level = 1;
        currentXP = 0;
        maxXP = level * expMutiplier;
    }

    public void IncrementXP(float amount)
    {
        currentXP += amount;

        if (currentXP >= maxXP)
            LevelUP();
    }

    public float GetXPPercent()
    {
        return currentXP / maxXP;
    }

    private void LevelUP()
    {
        currentXP -= maxXP;
        level++;
        maxXP = level * expMutiplier;

        playVFX.ShowLevelUpVFX(level);
        newLevelUp = true;
    }

    public int GetLevel() => level;
}
