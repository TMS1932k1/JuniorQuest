using System;
using UnityEngine;

public class Player_XP : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] int level;
    [SerializeField] float currentXP;
    [SerializeField] float expMutiplier = 100f;
    [SerializeField] UI_LevelUp lvUpUI;
    private float maxXP;


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

        lvUpUI.ShowText(level);
    }

    public int GetLevel() => level;
}
