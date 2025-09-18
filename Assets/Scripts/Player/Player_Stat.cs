using UnityEngine;

public class Player_Stat : Entity_Stat, ISaveable
{
    public void LoadData(GameData gameData)
    {
        AddModifierWithType(EStat_Type.Strength, SourceStatStrings.POINT_SOURCE, gameData.strength);
        AddModifierWithType(EStat_Type.Agility, SourceStatStrings.POINT_SOURCE, gameData.agility);
        AddModifierWithType(EStat_Type.Vitality, SourceStatStrings.POINT_SOURCE, gameData.vitality);

        haveChange = true;
    }

    public void SaveDate(ref GameData gameData)
    {
        gameData.strength = major.strength.GetValue();
        gameData.agility = major.agility.GetValue();
        gameData.vitality = major.vitality.GetValue();
    }
}
