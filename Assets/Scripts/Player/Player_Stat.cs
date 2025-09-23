using UnityEngine;

public class Player_Stat : Entity_Stat, ISaveable
{
    public void LoadData(GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Load Major Point of Player");

        RemoveModifierWithType(EStat_Type.Strength, SourceStatStrings.POINT_SOURCE);
        RemoveModifierWithType(EStat_Type.Agility, SourceStatStrings.POINT_SOURCE);
        RemoveModifierWithType(EStat_Type.Vitality, SourceStatStrings.POINT_SOURCE);

        AddModifierWithType(EStat_Type.Strength, SourceStatStrings.POINT_SOURCE, gameData.strength);
        AddModifierWithType(EStat_Type.Agility, SourceStatStrings.POINT_SOURCE, gameData.agility);
        AddModifierWithType(EStat_Type.Vitality, SourceStatStrings.POINT_SOURCE, gameData.vitality);

        haveChange = true;
    }

    public void SaveData(ref GameData gameData)
    {
        Debug.Log($"SAVE_MANAGER: Save Major Point of Player");

        gameData.strength = major.strength.GetValue();
        gameData.agility = major.agility.GetValue();
        gameData.vitality = major.vitality.GetValue();
    }
}
