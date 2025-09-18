using System;
using UnityEngine;

public class Player_XP : MonoBehaviour, ISaveable
{
    [Range(1, 100)]
    [SerializeField] int level;
    [SerializeField] float currentXP;
    [SerializeField] float expMutiplier = 50f;

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
        {
            HandleLevelUP();
            newLevelUp = true;
        }
    }

    public float GetXPPercent()
    {
        return currentXP / maxXP;
    }

    private void HandleLevelUP()
    {
        while (currentXP >= maxXP)
        {
            currentXP -= maxXP;
            level++;
            maxXP = level * expMutiplier;
        }

        playVFX.ShowLevelUpVFX(level);
    }

    public int GetLevel() => level;

    public void SaveDate(ref GameData gameData)
    {
        gameData.playerLevel = level;
        gameData.playerXP = currentXP;
    }

    public void LoadData(GameData gameData)
    {
        level = gameData.playerLevel;
        maxXP = level * expMutiplier;
        currentXP = gameData.playerXP;

        newLevelUp = true;
    }
}
