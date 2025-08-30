using UnityEngine;

public interface ICanBurn
{
    public bool GetCanBurn();
    public void BeBurn(float damage, float duration, int countHit);
}
