using UnityEngine;

public interface IEntitySaveable
{
    public void HandleLoad(GameData gameData);
    public void HandleSave(ref GameData gameData);
}
