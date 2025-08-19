using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [Header("Counter Alert")]
    [SerializeField] private GameObject CounterAlert;

    public void EnableCounterAlert(bool enable)
    {
        CounterAlert.SetActive(enable);
    }
}
