using UnityEngine;

public interface ISaveable
{
    public void SaveDate(ref GameData gameData);

    public void LoadData(GameData gameData);
}
